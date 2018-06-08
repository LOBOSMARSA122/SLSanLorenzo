namespace Sigesoft.Node.WinClient.UI
{
    partial class frmOccupationalHistory
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ParentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DangerName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_TypeofEEPName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_Percentage");
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOccupationalHistory));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtActividad = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkPuestoActual = new System.Windows.Forms.CheckBox();
            this.ddlTypeOperationId = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOccupation = new System.Windows.Forms.TextBox();
            this.txtGeographicalHeight = new System.Windows.Forms.TextBox();
            this.txtTypeActivity = new System.Windows.Forms.TextBox();
            this.txtOrganization = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dptDateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTimeStar = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeViewDangers = new System.Windows.Forms.TreeView();
            this.btnDeleteDanger = new System.Windows.Forms.Button();
            this.btnMoveDanger = new System.Windows.Forms.Button();
            this.grdDataDangers = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeViewEPP = new System.Windows.Forms.TreeView();
            this.btnDeleteEPP = new System.Windows.Forms.Button();
            this.btnMoveEPP = new System.Windows.Forms.Button();
            this.grdDataEPP = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.contextMenuTypeEPP = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ultraValidator1 = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataDangers)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataEPP)).BeginInit();
            this.contextMenuTypeEPP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraValidator1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtActividad);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.chkPuestoActual);
            this.groupBox1.Controls.Add(this.ddlTypeOperationId);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtOccupation);
            this.groupBox1.Controls.Add(this.txtGeographicalHeight);
            this.groupBox1.Controls.Add(this.txtTypeActivity);
            this.groupBox1.Controls.Add(this.txtOrganization);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dptDateTimeEnd);
            this.groupBox1.Controls.Add(this.dtpDateTimeStar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(20, 19);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(990, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales";
            // 
            // txtActividad
            // 
            this.txtActividad.Location = new System.Drawing.Point(81, 71);
            this.txtActividad.Margin = new System.Windows.Forms.Padding(2);
            this.txtActividad.MaxLength = 250;
            this.txtActividad.Name = "txtActividad";
            this.txtActividad.Size = new System.Drawing.Size(281, 20);
            this.txtActividad.TabIndex = 20;
            this.ultraValidator1.GetValidationSettings(this.txtActividad).DataType = typeof(string);
            this.ultraValidator1.GetValidationSettings(this.txtActividad).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.ultraValidator1.GetValidationSettings(this.txtActividad).IsRequired = true;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(15, 72);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 19);
            this.label9.TabIndex = 21;
            this.label9.Text = "Actividad";
            // 
            // chkPuestoActual
            // 
            this.chkPuestoActual.AutoSize = true;
            this.chkPuestoActual.Location = new System.Drawing.Point(379, 50);
            this.chkPuestoActual.Name = "chkPuestoActual";
            this.chkPuestoActual.Size = new System.Drawing.Size(89, 17);
            this.chkPuestoActual.TabIndex = 19;
            this.chkPuestoActual.Text = "PuestoActual";
            this.chkPuestoActual.UseVisualStyleBackColor = true;
            // 
            // ddlTypeOperationId
            // 
            this.ddlTypeOperationId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlTypeOperationId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlTypeOperationId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlTypeOperationId.FormattingEnabled = true;
            this.ddlTypeOperationId.Location = new System.Drawing.Point(808, 46);
            this.ddlTypeOperationId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlTypeOperationId.Name = "ddlTypeOperationId";
            this.ddlTypeOperationId.Size = new System.Drawing.Size(150, 21);
            this.ddlTypeOperationId.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(711, 54);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Tipo Operación";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(655, 54);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "mt";
            // 
            // txtOccupation
            // 
            this.txtOccupation.Location = new System.Drawing.Point(81, 47);
            this.txtOccupation.Margin = new System.Windows.Forms.Padding(2);
            this.txtOccupation.MaxLength = 250;
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.Size = new System.Drawing.Size(281, 20);
            this.txtOccupation.TabIndex = 5;
            this.ultraValidator1.GetValidationSettings(this.txtOccupation).DataType = typeof(string);
            this.ultraValidator1.GetValidationSettings(this.txtOccupation).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.ultraValidator1.GetValidationSettings(this.txtOccupation).IsRequired = true;
            // 
            // txtGeographicalHeight
            // 
            this.txtGeographicalHeight.Location = new System.Drawing.Point(604, 47);
            this.txtGeographicalHeight.Margin = new System.Windows.Forms.Padding(2);
            this.txtGeographicalHeight.MaxLength = 5;
            this.txtGeographicalHeight.Name = "txtGeographicalHeight";
            this.txtGeographicalHeight.Size = new System.Drawing.Size(47, 20);
            this.txtGeographicalHeight.TabIndex = 6;
            this.ultraValidator1.GetValidationSettings(this.txtGeographicalHeight).DataType = typeof(int);
            this.txtGeographicalHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGeographicalHeight_KeyPress);
            // 
            // txtTypeActivity
            // 
            this.txtTypeActivity.Location = new System.Drawing.Point(808, 16);
            this.txtTypeActivity.Margin = new System.Windows.Forms.Padding(2);
            this.txtTypeActivity.MaxLength = 250;
            this.txtTypeActivity.Name = "txtTypeActivity";
            this.txtTypeActivity.Size = new System.Drawing.Size(150, 20);
            this.txtTypeActivity.TabIndex = 4;
            this.ultraValidator1.GetValidationSettings(this.txtTypeActivity).DataType = typeof(string);
            this.ultraValidator1.GetValidationSettings(this.txtTypeActivity).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.ultraValidator1.GetValidationSettings(this.txtTypeActivity).IsRequired = true;
            // 
            // txtOrganization
            // 
            this.txtOrganization.Location = new System.Drawing.Point(470, 16);
            this.txtOrganization.Margin = new System.Windows.Forms.Padding(2);
            this.txtOrganization.MaxLength = 250;
            this.txtOrganization.Name = "txtOrganization";
            this.txtOrganization.Size = new System.Drawing.Size(269, 20);
            this.txtOrganization.TabIndex = 3;
            this.ultraValidator1.GetValidationSettings(this.txtOrganization).DataType = typeof(string);
            this.ultraValidator1.GetValidationSettings(this.txtOrganization).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.ultraValidator1.GetValidationSettings(this.txtOrganization).IsRequired = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 48);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "Puesto";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(502, 52);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Altura Geográfica";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(762, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Área";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(418, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Empresa";
            // 
            // dptDateTimeEnd
            // 
            this.dptDateTimeEnd.CustomFormat = "MMMM/yyyy";
            this.dptDateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptDateTimeEnd.Location = new System.Drawing.Point(277, 16);
            this.dptDateTimeEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dptDateTimeEnd.Name = "dptDateTimeEnd";
            this.dptDateTimeEnd.ShowUpDown = true;
            this.dptDateTimeEnd.Size = new System.Drawing.Size(102, 20);
            this.dptDateTimeEnd.TabIndex = 2;
            this.dptDateTimeEnd.Validating += new System.ComponentModel.CancelEventHandler(this.dptDateTimeEnd_Validating);
            // 
            // dtpDateTimeStar
            // 
            this.dtpDateTimeStar.CustomFormat = "MMMM/yyyy";
            this.dtpDateTimeStar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTimeStar.Location = new System.Drawing.Point(81, 16);
            this.dtpDateTimeStar.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDateTimeStar.Name = "dtpDateTimeStar";
            this.dtpDateTimeStar.ShowUpDown = true;
            this.dtpDateTimeStar.Size = new System.Drawing.Size(102, 20);
            this.dtpDateTimeStar.TabIndex = 1;
            this.dtpDateTimeStar.Validating += new System.ComponentModel.CancelEventHandler(this.dtpDateTimeStar_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Fecha Fin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Fecha Inicio";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.treeViewDangers);
            this.groupBox2.Controls.Add(this.btnDeleteDanger);
            this.groupBox2.Controls.Add(this.btnMoveDanger);
            this.groupBox2.Controls.Add(this.grdDataDangers);
            this.groupBox2.Location = new System.Drawing.Point(20, 126);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(489, 362);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Peligros en el Puesto";
            // 
            // treeViewDangers
            // 
            this.treeViewDangers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewDangers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewDangers.Location = new System.Drawing.Point(4, 18);
            this.treeViewDangers.Margin = new System.Windows.Forms.Padding(2);
            this.treeViewDangers.Name = "treeViewDangers";
            this.treeViewDangers.Size = new System.Drawing.Size(216, 340);
            this.treeViewDangers.TabIndex = 48;
            // 
            // btnDeleteDanger
            // 
            this.btnDeleteDanger.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnDeleteDanger.Location = new System.Drawing.Point(224, 172);
            this.btnDeleteDanger.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteDanger.Name = "btnDeleteDanger";
            this.btnDeleteDanger.Size = new System.Drawing.Size(25, 20);
            this.btnDeleteDanger.TabIndex = 47;
            this.btnDeleteDanger.UseVisualStyleBackColor = true;
            this.btnDeleteDanger.Click += new System.EventHandler(this.btnDeleteDanger_Click);
            // 
            // btnMoveDanger
            // 
            this.btnMoveDanger.Image = global::Sigesoft.Node.WinClient.UI.Resources.play_green;
            this.btnMoveDanger.Location = new System.Drawing.Point(224, 132);
            this.btnMoveDanger.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveDanger.Name = "btnMoveDanger";
            this.btnMoveDanger.Size = new System.Drawing.Size(25, 20);
            this.btnMoveDanger.TabIndex = 46;
            this.btnMoveDanger.UseVisualStyleBackColor = true;
            this.btnMoveDanger.Click += new System.EventHandler(this.btnMoveDanger_Click);
            // 
            // grdDataDangers
            // 
            this.grdDataDangers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataDangers.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.LightGray;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataDangers.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Categoría";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Width = 171;
            ultraGridColumn6.Header.Caption = "Peligro";
            ultraGridColumn6.Header.VisiblePosition = 0;
            ultraGridColumn6.Width = 167;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn6});
            this.grdDataDangers.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataDangers.DisplayLayout.InterBandSpacing = 10;
            this.grdDataDangers.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataDangers.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataDangers.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataDangers.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataDangers.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataDangers.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataDangers.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataDangers.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDataDangers.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataDangers.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDataDangers.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataDangers.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDataDangers.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataDangers.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataDangers.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDataDangers.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdDataDangers.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDataDangers.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataDangers.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataDangers.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataDangers.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataDangers.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataDangers.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataDangers.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataDangers.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataDangers.Location = new System.Drawing.Point(254, 17);
            this.grdDataDangers.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataDangers.Name = "grdDataDangers";
            this.grdDataDangers.Size = new System.Drawing.Size(231, 340);
            this.grdDataDangers.TabIndex = 44;
            this.grdDataDangers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdDataDangers_MouseDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.treeViewEPP);
            this.groupBox3.Controls.Add(this.btnDeleteEPP);
            this.groupBox3.Controls.Add(this.btnMoveEPP);
            this.groupBox3.Controls.Add(this.grdDataEPP);
            this.groupBox3.Location = new System.Drawing.Point(524, 129);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(486, 359);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de EPP usado";
            // 
            // treeViewEPP
            // 
            this.treeViewEPP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewEPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewEPP.Location = new System.Drawing.Point(4, 19);
            this.treeViewEPP.Margin = new System.Windows.Forms.Padding(2);
            this.treeViewEPP.Name = "treeViewEPP";
            this.treeViewEPP.Size = new System.Drawing.Size(202, 338);
            this.treeViewEPP.TabIndex = 52;
            // 
            // btnDeleteEPP
            // 
            this.btnDeleteEPP.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnDeleteEPP.Location = new System.Drawing.Point(210, 169);
            this.btnDeleteEPP.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteEPP.Name = "btnDeleteEPP";
            this.btnDeleteEPP.Size = new System.Drawing.Size(25, 20);
            this.btnDeleteEPP.TabIndex = 51;
            this.btnDeleteEPP.UseVisualStyleBackColor = true;
            this.btnDeleteEPP.Click += new System.EventHandler(this.btnDeleteEPP_Click);
            // 
            // btnMoveEPP
            // 
            this.btnMoveEPP.Image = global::Sigesoft.Node.WinClient.UI.Resources.play_green;
            this.btnMoveEPP.Location = new System.Drawing.Point(210, 128);
            this.btnMoveEPP.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveEPP.Name = "btnMoveEPP";
            this.btnMoveEPP.Size = new System.Drawing.Size(25, 20);
            this.btnMoveEPP.TabIndex = 50;
            this.btnMoveEPP.UseVisualStyleBackColor = true;
            this.btnMoveEPP.Click += new System.EventHandler(this.btnMoveEPP_Click);
            // 
            // grdDataEPP
            // 
            this.grdDataEPP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataEPP.CausesValidation = false;
            this.grdDataEPP.ContextMenuStrip = this.contextMenuTypeEPP;
            appearance8.BackColor = System.Drawing.Color.White;
            appearance8.BackColor2 = System.Drawing.Color.LightGray;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataEPP.DisplayLayout.Appearance = appearance8;
            ultraGridColumn2.Header.Caption = "Tipo EPP";
            ultraGridColumn2.Header.VisiblePosition = 0;
            ultraGridColumn2.Width = 182;
            ultraGridColumn5.Header.Caption = "%";
            ultraGridColumn5.Header.VisiblePosition = 1;
            ultraGridColumn5.Width = 42;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn2,
            ultraGridColumn5});
            this.grdDataEPP.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdDataEPP.DisplayLayout.InterBandSpacing = 10;
            this.grdDataEPP.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataEPP.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataEPP.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataEPP.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataEPP.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataEPP.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataEPP.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataEPP.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance9.BackColor = System.Drawing.Color.Transparent;
            this.grdDataEPP.DisplayLayout.Override.CardAreaAppearance = appearance9;
            appearance10.BackColor = System.Drawing.Color.White;
            appearance10.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataEPP.DisplayLayout.Override.CellAppearance = appearance10;
            this.grdDataEPP.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.Color.White;
            appearance11.BackColor2 = System.Drawing.Color.LightGray;
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance11.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataEPP.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.grdDataEPP.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance12.AlphaLevel = ((short)(187));
            appearance12.BackColor = System.Drawing.Color.Gainsboro;
            appearance12.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance12.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataEPP.DisplayLayout.Override.RowAlternateAppearance = appearance12;
            appearance13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataEPP.DisplayLayout.Override.RowSelectorAppearance = appearance13;
            this.grdDataEPP.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance14.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance14.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance14.FontData.BoldAsString = "False";
            appearance14.ForeColor = System.Drawing.Color.Black;
            this.grdDataEPP.DisplayLayout.Override.SelectedRowAppearance = appearance14;
            this.grdDataEPP.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataEPP.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataEPP.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataEPP.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataEPP.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataEPP.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataEPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataEPP.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataEPP.Location = new System.Drawing.Point(246, 17);
            this.grdDataEPP.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataEPP.Name = "grdDataEPP";
            this.grdDataEPP.Size = new System.Drawing.Size(236, 337);
            this.grdDataEPP.TabIndex = 49;
            this.grdDataEPP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdDataEPP_MouseDown);
            // 
            // contextMenuTypeEPP
            // 
            this.contextMenuTypeEPP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarToolStripMenuItem});
            this.contextMenuTypeEPP.Name = "contextMenuTypeEPP";
            this.contextMenuTypeEPP.Size = new System.Drawing.Size(105, 26);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("modificarToolStripMenuItem.Image")));
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.modificarToolStripMenuItem.Text = "Editar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(931, 488);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Salir";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(840, 488);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 24);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "Grabar";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmOccupationalHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1026, 523);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOccupationalHistory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Antecedentes Ocupacionales";
            this.Load += new System.EventHandler(this.frmOccupationalHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataDangers)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataEPP)).EndInit();
            this.contextMenuTypeEPP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraValidator1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGeographicalHeight;
        private System.Windows.Forms.TextBox txtTypeActivity;
        private System.Windows.Forms.TextBox txtOrganization;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dptDateTimeEnd;
        private System.Windows.Forms.DateTimePicker dtpDateTimeStar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOccupation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCancel;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataDangers;
        private System.Windows.Forms.Button btnDeleteDanger;
        private System.Windows.Forms.Button btnMoveDanger;
        private System.Windows.Forms.TreeView treeViewDangers;
        private System.Windows.Forms.TreeView treeViewEPP;
        private System.Windows.Forms.Button btnDeleteEPP;
        private System.Windows.Forms.Button btnMoveEPP;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataEPP;
        private Infragistics.Win.Misc.UltraValidator ultraValidator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuTypeEPP;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        public System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ddlTypeOperationId;
        private System.Windows.Forms.CheckBox chkPuestoActual;
        private System.Windows.Forms.TextBox txtActividad;
        private System.Windows.Forms.Label label9;
    }
}