namespace Sigesoft.Node.WinClient.UI
{
    partial class frmEspecialista
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CreationUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_CreationDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_ServiceDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_UpdateUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_UpdateDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_MasterServiceName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceStatusName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_OrganizationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_LocationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Pacient");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_AptitudeStatusName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Liq", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlCustomerOrganization = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ddComponentStatusId = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPacient = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dptDateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTimeStar = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRecordCountCalendar = new System.Windows.Forms.Label();
            this.grdDataService = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnCulminarExamen = new System.Windows.Forms.Button();
            this.btnInterconsulta = new System.Windows.Forms.Button();
            this.btnGenerarLiquidacion = new System.Windows.Forms.Button();
            this.btnAdminReportes = new System.Windows.Forms.Button();
            this.btnEditarESO = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataService)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnFilter);
            this.groupBox1.Controls.Add(this.ddlCustomerOrganization);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.ddComponentStatusId);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtPacient);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dptDateTimeEnd);
            this.groupBox1.Controls.Add(this.dtpDateTimeStar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(11, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(777, 73);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // ddlCustomerOrganization
            // 
            this.ddlCustomerOrganization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCustomerOrganization.DropDownWidth = 400;
            this.ddlCustomerOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCustomerOrganization.FormattingEnabled = true;
            this.ddlCustomerOrganization.Location = new System.Drawing.Point(96, 42);
            this.ddlCustomerOrganization.Margin = new System.Windows.Forms.Padding(2);
            this.ddlCustomerOrganization.Name = "ddlCustomerOrganization";
            this.ddlCustomerOrganization.Size = new System.Drawing.Size(343, 21);
            this.ddlCustomerOrganization.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(9, 45);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "Empresa Cliente";
            // 
            // ddComponentStatusId
            // 
            this.ddComponentStatusId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddComponentStatusId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddComponentStatusId.FormattingEnabled = true;
            this.ddComponentStatusId.Location = new System.Drawing.Point(555, 42);
            this.ddComponentStatusId.Margin = new System.Windows.Forms.Padding(2);
            this.ddComponentStatusId.Name = "ddComponentStatusId";
            this.ddComponentStatusId.Size = new System.Drawing.Size(109, 21);
            this.ddComponentStatusId.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(453, 47);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Estado del Examen";
            // 
            // txtPacient
            // 
            this.txtPacient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacient.Location = new System.Drawing.Point(456, 18);
            this.txtPacient.Margin = new System.Windows.Forms.Padding(2);
            this.txtPacient.Name = "txtPacient";
            this.txtPacient.Size = new System.Drawing.Size(208, 20);
            this.txtPacient.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(313, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Paciente / Nro Documento";
            // 
            // dptDateTimeEnd
            // 
            this.dptDateTimeEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptDateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptDateTimeEnd.Location = new System.Drawing.Point(209, 17);
            this.dptDateTimeEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dptDateTimeEnd.Name = "dptDateTimeEnd";
            this.dptDateTimeEnd.Size = new System.Drawing.Size(95, 20);
            this.dptDateTimeEnd.TabIndex = 3;
            // 
            // dtpDateTimeStar
            // 
            this.dtpDateTimeStar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateTimeStar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTimeStar.Location = new System.Drawing.Point(96, 17);
            this.dtpDateTimeStar.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDateTimeStar.Name = "dtpDateTimeStar";
            this.dtpDateTimeStar.Size = new System.Drawing.Size(95, 20);
            this.dtpDateTimeStar.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(194, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha Atención";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblRecordCountCalendar);
            this.groupBox2.Controls.Add(this.grdDataService);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(11, 88);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(777, 344);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Servicios";
            // 
            // lblRecordCountCalendar
            // 
            this.lblRecordCountCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountCalendar.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCountCalendar.Location = new System.Drawing.Point(535, 8);
            this.lblRecordCountCalendar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountCalendar.Name = "lblRecordCountCalendar";
            this.lblRecordCountCalendar.Size = new System.Drawing.Size(231, 19);
            this.lblRecordCountCalendar.TabIndex = 52;
            this.lblRecordCountCalendar.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountCalendar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdDataService
            // 
            this.grdDataService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataService.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataService.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Id Atención";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Width = 111;
            ultraGridColumn20.Header.Caption = "Usuario Crea.";
            ultraGridColumn20.Header.VisiblePosition = 11;
            ultraGridColumn20.Width = 125;
            ultraGridColumn21.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn21.Header.Caption = "Fecha Crea.";
            ultraGridColumn21.Header.VisiblePosition = 12;
            ultraGridColumn21.Width = 150;
            ultraGridColumn6.Header.Caption = "Fecha";
            ultraGridColumn6.Header.VisiblePosition = 3;
            ultraGridColumn6.Width = 74;
            ultraGridColumn22.Header.Caption = "Usuario Act.";
            ultraGridColumn22.Header.VisiblePosition = 13;
            ultraGridColumn22.Width = 125;
            ultraGridColumn23.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn23.Header.Caption = "Fecha Act.";
            ultraGridColumn23.Header.VisiblePosition = 14;
            ultraGridColumn23.Width = 150;
            ultraGridColumn3.Header.Caption = "Servicio";
            ultraGridColumn3.Header.VisiblePosition = 4;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn3.Width = 178;
            ultraGridColumn7.Header.Caption = "Estado Servicio";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn8.Header.Caption = "Empresa";
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn8.Width = 199;
            ultraGridColumn9.Header.Caption = "Sede";
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn9.Width = 137;
            ultraGridColumn10.Header.Caption = "Tipo Servicio";
            ultraGridColumn10.Header.VisiblePosition = 5;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn4.Header.Caption = "Protocolo";
            ultraGridColumn4.Header.VisiblePosition = 10;
            ultraGridColumn4.Width = 239;
            ultraGridColumn2.Header.Caption = "Paciente";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Width = 234;
            ultraGridColumn5.Header.Caption = "Aptitud";
            ultraGridColumn5.Header.VisiblePosition = 7;
            appearance2.TextHAlignAsString = "Right";
            ultraGridColumn11.Header.Appearance = appearance2;
            ultraGridColumn11.Header.ToolTipText = "Pre-Liquidación";
            ultraGridColumn11.Header.VisiblePosition = 0;
            ultraGridColumn11.Width = 20;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn6,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn3,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn4,
            ultraGridColumn2,
            ultraGridColumn5,
            ultraGridColumn11});
            this.grdDataService.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataService.DisplayLayout.InterBandSpacing = 10;
            this.grdDataService.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataService.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataService.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataService.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataService.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataService.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataService.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataService.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataService.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.grdDataService.DisplayLayout.Override.CardAreaAppearance = appearance3;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.White;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataService.DisplayLayout.Override.CellAppearance = appearance4;
            this.grdDataService.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance5.BackColor = System.Drawing.Color.White;
            appearance5.BackColor2 = System.Drawing.Color.LightGray;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance5.BorderColor = System.Drawing.Color.DarkGray;
            appearance5.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataService.DisplayLayout.Override.HeaderAppearance = appearance5;
            this.grdDataService.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance6.AlphaLevel = ((short)(187));
            appearance6.BackColor = System.Drawing.Color.Gainsboro;
            appearance6.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance6.ForeColor = System.Drawing.Color.Black;
            appearance6.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataService.DisplayLayout.Override.RowAlternateAppearance = appearance6;
            appearance7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataService.DisplayLayout.Override.RowSelectorAppearance = appearance7;
            this.grdDataService.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance8.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance8.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance8.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance8.FontData.BoldAsString = "False";
            appearance8.ForeColor = System.Drawing.Color.Black;
            this.grdDataService.DisplayLayout.Override.SelectedRowAppearance = appearance8;
            this.grdDataService.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataService.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataService.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
            this.grdDataService.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataService.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataService.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataService.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataService.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataService.Location = new System.Drawing.Point(14, 28);
            this.grdDataService.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataService.Name = "grdDataService";
            this.grdDataService.Size = new System.Drawing.Size(752, 306);
            this.grdDataService.TabIndex = 44;
            this.grdDataService.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdDataService_AfterSelectChange);
            // 
            // btnCulminarExamen
            // 
            this.btnCulminarExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCulminarExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnCulminarExamen.Enabled = false;
            this.btnCulminarExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCulminarExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCulminarExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCulminarExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCulminarExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCulminarExamen.ForeColor = System.Drawing.Color.Black;
            this.btnCulminarExamen.Image = global::Sigesoft.Node.WinClient.UI.Resources.cog;
            this.btnCulminarExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCulminarExamen.Location = new System.Drawing.Point(792, 263);
            this.btnCulminarExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnCulminarExamen.Name = "btnCulminarExamen";
            this.btnCulminarExamen.Size = new System.Drawing.Size(85, 41);
            this.btnCulminarExamen.TabIndex = 133;
            this.btnCulminarExamen.Text = "Culminar Examen ";
            this.btnCulminarExamen.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnCulminarExamen.UseVisualStyleBackColor = false;
            this.btnCulminarExamen.Click += new System.EventHandler(this.btnCulminarExamen_Click);
            // 
            // btnInterconsulta
            // 
            this.btnInterconsulta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInterconsulta.BackColor = System.Drawing.SystemColors.Control;
            this.btnInterconsulta.Enabled = false;
            this.btnInterconsulta.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnInterconsulta.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnInterconsulta.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnInterconsulta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInterconsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInterconsulta.ForeColor = System.Drawing.Color.Black;
            this.btnInterconsulta.Image = global::Sigesoft.Node.WinClient.UI.Resources.note_add;
            this.btnInterconsulta.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInterconsulta.Location = new System.Drawing.Point(792, 220);
            this.btnInterconsulta.Margin = new System.Windows.Forms.Padding(2);
            this.btnInterconsulta.Name = "btnInterconsulta";
            this.btnInterconsulta.Size = new System.Drawing.Size(85, 39);
            this.btnInterconsulta.TabIndex = 132;
            this.btnInterconsulta.Text = "Diagnosticar";
            this.btnInterconsulta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInterconsulta.UseVisualStyleBackColor = false;
            this.btnInterconsulta.Click += new System.EventHandler(this.btnInterconsulta_Click);
            // 
            // btnGenerarLiquidacion
            // 
            this.btnGenerarLiquidacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarLiquidacion.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenerarLiquidacion.Enabled = false;
            this.btnGenerarLiquidacion.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGenerarLiquidacion.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGenerarLiquidacion.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGenerarLiquidacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarLiquidacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarLiquidacion.ForeColor = System.Drawing.Color.Black;
            this.btnGenerarLiquidacion.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.btnGenerarLiquidacion.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGenerarLiquidacion.Location = new System.Drawing.Point(792, 178);
            this.btnGenerarLiquidacion.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerarLiquidacion.Name = "btnGenerarLiquidacion";
            this.btnGenerarLiquidacion.Size = new System.Drawing.Size(85, 38);
            this.btnGenerarLiquidacion.TabIndex = 131;
            this.btnGenerarLiquidacion.Text = "Registrar";
            this.btnGenerarLiquidacion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGenerarLiquidacion.UseVisualStyleBackColor = false;
            this.btnGenerarLiquidacion.Click += new System.EventHandler(this.btnGenerarLiquidacion_Click);
            // 
            // btnAdminReportes
            // 
            this.btnAdminReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminReportes.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdminReportes.Enabled = false;
            this.btnAdminReportes.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAdminReportes.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAdminReportes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAdminReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminReportes.ForeColor = System.Drawing.Color.Black;
            this.btnAdminReportes.Image = global::Sigesoft.Node.WinClient.UI.Resources.color_swatch;
            this.btnAdminReportes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdminReportes.Location = new System.Drawing.Point(792, 136);
            this.btnAdminReportes.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdminReportes.Name = "btnAdminReportes";
            this.btnAdminReportes.Size = new System.Drawing.Size(85, 38);
            this.btnAdminReportes.TabIndex = 129;
            this.btnAdminReportes.Text = "Ver Ficha";
            this.btnAdminReportes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdminReportes.UseVisualStyleBackColor = false;
            this.btnAdminReportes.Click += new System.EventHandler(this.btnAdminReportes_Click);
            // 
            // btnEditarESO
            // 
            this.btnEditarESO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditarESO.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditarESO.Enabled = false;
            this.btnEditarESO.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditarESO.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEditarESO.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEditarESO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarESO.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarESO.ForeColor = System.Drawing.Color.Black;
            this.btnEditarESO.Image = global::Sigesoft.Node.WinClient.UI.Resources.application_edit;
            this.btnEditarESO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditarESO.Location = new System.Drawing.Point(792, 96);
            this.btnEditarESO.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditarESO.Name = "btnEditarESO";
            this.btnEditarESO.Size = new System.Drawing.Size(85, 36);
            this.btnEditarESO.TabIndex = 51;
            this.btnEditarESO.Text = " Descargar Archivo";
            this.btnEditarESO.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnEditarESO.UseVisualStyleBackColor = false;
            this.btnEditarESO.Click += new System.EventHandler(this.btnEditarESO_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.SystemColors.Control;
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFilter.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFilter.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Image = global::Sigesoft.Node.WinClient.UI.Resources.find;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(684, 39);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 24);
            this.btnFilter.TabIndex = 106;
            this.btnFilter.Text = "Filtrar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // frmEspecialista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(895, 443);
            this.Controls.Add(this.btnCulminarExamen);
            this.Controls.Add(this.btnInterconsulta);
            this.Controls.Add(this.btnGenerarLiquidacion);
            this.Controls.Add(this.btnAdminReportes);
            this.Controls.Add(this.btnEditarESO);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEspecialista";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Especialista";
            this.Load += new System.EventHandler(this.frmEspecialista_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataService)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox ddlCustomerOrganization;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox ddComponentStatusId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPacient;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dptDateTimeEnd;
        private System.Windows.Forms.DateTimePicker dtpDateTimeStar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRecordCountCalendar;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataService;
        private System.Windows.Forms.Button btnEditarESO;
        private System.Windows.Forms.Button btnAdminReportes;
        private System.Windows.Forms.Button btnGenerarLiquidacion;
        private System.Windows.Forms.Button btnInterconsulta;
        private System.Windows.Forms.Button btnCulminarExamen;
    }
}