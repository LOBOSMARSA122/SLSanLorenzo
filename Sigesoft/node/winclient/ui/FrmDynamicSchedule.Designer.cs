namespace Sigesoft.Node.WinClient.UI
{
    partial class FrmDynamicSchedule
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NroDocument");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SecondLastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Birthdate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SexTypeId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CurrentOccupation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GesoId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Geso");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Select", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Clone", 1);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDynamicSchedule));
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            this.grdSchedule = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkFC = new System.Windows.Forms.CheckBox();
            this.dtpCalendarDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbOrganizationInvoice = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbIntermediaryOrganization = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbEsoType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbOrganization = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ultraStatusBarSchedule = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
            this.uvschedule = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.btnChangeGeso = new System.Windows.Forms.Button();
            this.btnOrderRows = new System.Windows.Forms.Button();
            this.btnRemoveRow = new System.Windows.Forms.Button();
            this.btnStartSchedule = new System.Windows.Forms.Button();
            this.btnNewRow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraStatusBarSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvschedule)).BeginInit();
            this.SuspendLayout();
            // 
            // grdSchedule
            // 
            this.grdSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSchedule.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdSchedule.DisplayLayout.Appearance = appearance1;
            ultraGridColumn3.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            ultraGridColumn3.Header.Caption = "Nro. Documento";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn11.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
            ultraGridColumn11.Header.Caption = "Nombres";
            ultraGridColumn11.Header.VisiblePosition = 3;
            ultraGridColumn11.Width = 161;
            ultraGridColumn12.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
            ultraGridColumn12.Header.Caption = "Apellido Paterno";
            ultraGridColumn12.Header.VisiblePosition = 4;
            ultraGridColumn12.Width = 144;
            ultraGridColumn13.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
            ultraGridColumn13.Header.Caption = "Apellido Materno";
            ultraGridColumn13.Header.VisiblePosition = 5;
            ultraGridColumn13.Width = 125;
            ultraGridColumn14.Header.Caption = "Fecha Nacimiento";
            ultraGridColumn14.Header.VisiblePosition = 6;
            ultraGridColumn15.Header.Caption = "Sexo";
            ultraGridColumn15.Header.VisiblePosition = 7;
            ultraGridColumn16.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            ultraGridColumn16.Header.Caption = "Ocupación Actual";
            ultraGridColumn16.Header.VisiblePosition = 10;
            ultraGridColumn16.Width = 147;
            ultraGridColumn17.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
            ultraGridColumn17.Header.VisiblePosition = 8;
            ultraGridColumn18.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
            ultraGridColumn18.Header.VisiblePosition = 9;
            ultraGridColumn19.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            ultraGridColumn19.Header.Fixed = true;
            ultraGridColumn19.Header.VisiblePosition = 0;
            ultraGridColumn19.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn19.Width = 40;
            ultraGridColumn20.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            appearance2.Image = ((object)(resources.GetObject("appearance2.Image")));
            appearance2.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance2.ImageVAlign = Infragistics.Win.VAlign.Middle;
            ultraGridColumn20.CellButtonAppearance = appearance2;
            appearance3.Image = ((object)(resources.GetObject("appearance3.Image")));
            ultraGridColumn20.Header.Appearance = appearance3;
            ultraGridColumn20.Header.Fixed = true;
            ultraGridColumn20.Header.ToolTipText = "Clonar Registro";
            ultraGridColumn20.Header.VisiblePosition = 1;
            ultraGridColumn20.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ultraGridColumn20.Width = 71;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn3,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20});
            ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.Yes;
            this.grdSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdSchedule.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedule.DisplayLayout.GroupByBox.Prompt = " ";
            this.grdSchedule.DisplayLayout.InterBandSpacing = 10;
            this.grdSchedule.DisplayLayout.MaxColScrollRegions = 1;
            this.grdSchedule.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdSchedule.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdSchedule.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdSchedule.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdSchedule.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.grdSchedule.DisplayLayout.Override.CardAreaAppearance = appearance4;
            appearance5.BackColor = System.Drawing.Color.White;
            appearance5.BackColor2 = System.Drawing.Color.White;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdSchedule.DisplayLayout.Override.CellAppearance = appearance5;
            this.grdSchedule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdSchedule.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.grdSchedule.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance6.BackColor = System.Drawing.Color.White;
            appearance6.BackColor2 = System.Drawing.Color.LightGray;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance6.BorderColor = System.Drawing.Color.DarkGray;
            appearance6.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdSchedule.DisplayLayout.Override.HeaderAppearance = appearance6;
            this.grdSchedule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance7.AlphaLevel = ((short)(187));
            appearance7.BackColor = System.Drawing.Color.Gainsboro;
            appearance7.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance7.ForeColor = System.Drawing.Color.Black;
            appearance7.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdSchedule.DisplayLayout.Override.RowAlternateAppearance = appearance7;
            appearance8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdSchedule.DisplayLayout.Override.RowSelectorAppearance = appearance8;
            this.grdSchedule.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdSchedule.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.RowIndex;
            this.grdSchedule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdSchedule.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            appearance9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance9.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance9.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance9.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance9.FontData.BoldAsString = "False";
            appearance9.ForeColor = System.Drawing.Color.Black;
            this.grdSchedule.DisplayLayout.Override.SelectedRowAppearance = appearance9;
            this.grdSchedule.DisplayLayout.Override.SupportDataErrorInfo = Infragistics.Win.UltraWinGrid.SupportDataErrorInfo.RowsAndCells;
            this.grdSchedule.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdSchedule.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdSchedule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSchedule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSchedule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdSchedule.Location = new System.Drawing.Point(11, 88);
            this.grdSchedule.Margin = new System.Windows.Forms.Padding(2);
            this.grdSchedule.Name = "grdSchedule";
            this.grdSchedule.Size = new System.Drawing.Size(1053, 374);
            this.grdSchedule.TabIndex = 154;
            this.grdSchedule.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdSchedule_InitializeLayout);
            this.grdSchedule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdSchedule_KeyDown);
            this.grdSchedule.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdSchedule_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkFC);
            this.groupBox1.Controls.Add(this.dtpCalendarDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbOrganizationInvoice);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbIntermediaryOrganization);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbEsoType);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbOrganization);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(11, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1053, 70);
            this.groupBox1.TabIndex = 156;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // chkFC
            // 
            this.chkFC.AutoSize = true;
            this.chkFC.Location = new System.Drawing.Point(807, 47);
            this.chkFC.Name = "chkFC";
            this.chkFC.Size = new System.Drawing.Size(86, 17);
            this.chkFC.TabIndex = 114;
            this.chkFC.Text = "Es Campaña";
            this.chkFC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFC.UseVisualStyleBackColor = true;
            // 
            // dtpCalendarDate
            // 
            this.dtpCalendarDate.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCalendarDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCalendarDate.Location = new System.Drawing.Point(469, 39);
            this.dtpCalendarDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpCalendarDate.Name = "dtpCalendarDate";
            this.dtpCalendarDate.Size = new System.Drawing.Size(95, 21);
            this.dtpCalendarDate.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(385, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Fecha Atención";
            // 
            // cbOrganizationInvoice
            // 
            this.cbOrganizationInvoice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbOrganizationInvoice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbOrganizationInvoice.DropDownWidth = 500;
            this.cbOrganizationInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOrganizationInvoice.FormattingEnabled = true;
            this.cbOrganizationInvoice.Location = new System.Drawing.Point(76, 13);
            this.cbOrganizationInvoice.Name = "cbOrganizationInvoice";
            this.cbOrganizationInvoice.Size = new System.Drawing.Size(223, 21);
            this.cbOrganizationInvoice.TabIndex = 30;
            this.uvschedule.GetValidationSettings(this.cbOrganizationInvoice).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.cbOrganizationInvoice).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.cbOrganizationInvoice).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvschedule.GetValidationSettings(this.cbOrganizationInvoice).IsRequired = true;
            this.cbOrganizationInvoice.SelectedIndexChanged += new System.EventHandler(this.cbOrganizationInvoice_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(5, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Emp. Cliente";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbIntermediaryOrganization
            // 
            this.cbIntermediaryOrganization.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbIntermediaryOrganization.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbIntermediaryOrganization.DropDownWidth = 500;
            this.cbIntermediaryOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIntermediaryOrganization.FormattingEnabled = true;
            this.cbIntermediaryOrganization.Location = new System.Drawing.Point(813, 13);
            this.cbIntermediaryOrganization.Name = "cbIntermediaryOrganization";
            this.cbIntermediaryOrganization.Size = new System.Drawing.Size(223, 21);
            this.cbIntermediaryOrganization.TabIndex = 28;
            this.uvschedule.GetValidationSettings(this.cbIntermediaryOrganization).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.cbIntermediaryOrganization).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.cbIntermediaryOrganization).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvschedule.GetValidationSettings(this.cbIntermediaryOrganization).IsRequired = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(723, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Emp. de Trabajo";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbEsoType
            // 
            this.cbEsoType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEsoType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEsoType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEsoType.FormattingEnabled = true;
            this.cbEsoType.Location = new System.Drawing.Point(77, 37);
            this.cbEsoType.Name = "cbEsoType";
            this.cbEsoType.Size = new System.Drawing.Size(222, 21);
            this.cbEsoType.TabIndex = 26;
            this.uvschedule.GetValidationSettings(this.cbEsoType).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.cbEsoType).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.cbEsoType).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvschedule.GetValidationSettings(this.cbEsoType).IsRequired = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(5, 37);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Tipo de Exa.";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbOrganization
            // 
            this.cbOrganization.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbOrganization.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbOrganization.DropDownWidth = 500;
            this.cbOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOrganization.FormattingEnabled = true;
            this.cbOrganization.Location = new System.Drawing.Point(469, 13);
            this.cbOrganization.Name = "cbOrganization";
            this.cbOrganization.Size = new System.Drawing.Size(223, 21);
            this.cbOrganization.TabIndex = 24;
            this.uvschedule.GetValidationSettings(this.cbOrganization).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.cbOrganization).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.cbOrganization).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvschedule.GetValidationSettings(this.cbOrganization).IsRequired = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(315, 16);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Emp. Empleadora (Contratista)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ultraStatusBarSchedule
            // 
            this.ultraStatusBarSchedule.Location = new System.Drawing.Point(0, 496);
            this.ultraStatusBarSchedule.Name = "ultraStatusBarSchedule";
            ultraStatusPanel1.Key = "Validating";
            ultraStatusPanel1.ProgressBarInfo.Label = "Validando Registros";
            ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Adjustable;
            ultraStatusPanel1.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Progress;
            ultraStatusPanel1.Visible = false;
            ultraStatusPanel1.Width = 533;
            ultraStatusPanel2.Key = "Processing";
            ultraStatusPanel2.ProgressBarInfo.Label = "Procesando Registros";
            ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Adjustable;
            ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Progress;
            ultraStatusPanel2.Visible = false;
            ultraStatusPanel2.Width = 533;
            ultraStatusPanel3.Key = "scheduling";
            ultraStatusPanel3.ProgressBarInfo.Label = "Agendando";
            ultraStatusPanel3.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Adjustable;
            ultraStatusPanel3.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Progress;
            ultraStatusPanel3.Visible = false;
            ultraStatusPanel3.Width = 533;
            this.ultraStatusBarSchedule.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[] {
            ultraStatusPanel1,
            ultraStatusPanel2,
            ultraStatusPanel3});
            this.ultraStatusBarSchedule.Size = new System.Drawing.Size(1070, 23);
            this.ultraStatusBarSchedule.TabIndex = 158;
            this.ultraStatusBarSchedule.Text = "Barra de Progeso";
            // 
            // btnChangeGeso
            // 
            this.btnChangeGeso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeGeso.Image = global::Sigesoft.Node.WinClient.UI.Resources.report;
            this.btnChangeGeso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangeGeso.Location = new System.Drawing.Point(583, 467);
            this.btnChangeGeso.Name = "btnChangeGeso";
            this.btnChangeGeso.Size = new System.Drawing.Size(104, 23);
            this.btnChangeGeso.TabIndex = 161;
            this.btnChangeGeso.Text = "&Cambiar GESO";
            this.btnChangeGeso.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChangeGeso.UseVisualStyleBackColor = true;
            this.uvschedule.GetValidationSettings(this.btnChangeGeso).DataType = typeof(string);
            this.btnChangeGeso.Click += new System.EventHandler(this.btnChangeGeso_Click);
            // 
            // btnOrderRows
            // 
            this.btnOrderRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrderRows.Image = global::Sigesoft.Node.WinClient.UI.Resources.chart_organisation;
            this.btnOrderRows.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrderRows.Location = new System.Drawing.Point(693, 467);
            this.btnOrderRows.Name = "btnOrderRows";
            this.btnOrderRows.Size = new System.Drawing.Size(125, 23);
            this.btnOrderRows.TabIndex = 160;
            this.btnOrderRows.Text = "&Ordenar Registros";
            this.btnOrderRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOrderRows.UseVisualStyleBackColor = true;
            this.btnOrderRows.Click += new System.EventHandler(this.btnOrderRows_Click);
            // 
            // btnRemoveRow
            // 
            this.btnRemoveRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveRow.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.delete;
            this.btnRemoveRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveRow.Location = new System.Drawing.Point(824, 467);
            this.btnRemoveRow.Name = "btnRemoveRow";
            this.btnRemoveRow.Size = new System.Drawing.Size(114, 23);
            this.btnRemoveRow.TabIndex = 159;
            this.btnRemoveRow.Text = "&Eliminar Registro";
            this.btnRemoveRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemoveRow.UseVisualStyleBackColor = true;
            this.btnRemoveRow.Click += new System.EventHandler(this.btnRemoveRow_Click);
            // 
            // btnStartSchedule
            // 
            this.btnStartSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartSchedule.Image = global::Sigesoft.Node.WinClient.UI.Resources.cog;
            this.btnStartSchedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStartSchedule.Location = new System.Drawing.Point(12, 467);
            this.btnStartSchedule.Name = "btnStartSchedule";
            this.btnStartSchedule.Size = new System.Drawing.Size(122, 23);
            this.btnStartSchedule.TabIndex = 157;
            this.btnStartSchedule.Text = "&Iniciar Agendado ";
            this.btnStartSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStartSchedule.UseVisualStyleBackColor = true;
            this.btnStartSchedule.Click += new System.EventHandler(this.btnStartSchedule_Click);
            // 
            // btnNewRow
            // 
            this.btnNewRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewRow.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnNewRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewRow.Location = new System.Drawing.Point(944, 467);
            this.btnNewRow.Name = "btnNewRow";
            this.btnNewRow.Size = new System.Drawing.Size(114, 23);
            this.btnNewRow.TabIndex = 155;
            this.btnNewRow.Text = "&Agregar Registro";
            this.btnNewRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNewRow.UseVisualStyleBackColor = true;
            this.btnNewRow.Click += new System.EventHandler(this.btnNewRow_Click);
            // 
            // FrmDynamicSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 519);
            this.Controls.Add(this.btnChangeGeso);
            this.Controls.Add(this.btnOrderRows);
            this.Controls.Add(this.btnRemoveRow);
            this.Controls.Add(this.ultraStatusBarSchedule);
            this.Controls.Add(this.btnStartSchedule);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNewRow);
            this.Controls.Add(this.grdSchedule);
            this.Name = "FrmDynamicSchedule";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agendado Dinámico";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAgendaDinamica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraStatusBarSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvschedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdSchedule;
        private System.Windows.Forms.Button btnNewRow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStartSchedule;
        private Infragistics.Win.UltraWinStatusBar.UltraStatusBar ultraStatusBarSchedule;
        private System.Windows.Forms.Button btnRemoveRow;
        private System.Windows.Forms.Button btnOrderRows;
        private System.Windows.Forms.ComboBox cbOrganizationInvoice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbIntermediaryOrganization;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbEsoType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbOrganization;
        private System.Windows.Forms.Label label11;
        private Infragistics.Win.Misc.UltraValidator uvschedule;
        private System.Windows.Forms.DateTimePicker dtpCalendarDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkFC;
        private System.Windows.Forms.Button btnChangeGeso;
    }
}