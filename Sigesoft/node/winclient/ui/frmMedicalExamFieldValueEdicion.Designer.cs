namespace Sigesoft.Node.WinClient.UI
{
    partial class frmMedicalExamFieldValueEdicion
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_RestrictionName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CreationUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_CreationDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_UpdateUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_UpdateDate");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_RecomendationName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CreationUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_CreationDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_UpdateUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_UpdateDate");
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.Nombre = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlOperatorId = new System.Windows.Forms.ComboBox();
            this.uvMedicalExamFiedValues = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.ddlIsAnormal = new System.Windows.Forms.ComboBox();
            this.txtDiagnostic = new System.Windows.Forms.TextBox();
            this.txtAnalyzeValue1 = new System.Windows.Forms.TextBox();
            this.txtAnalyzeValue2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLegalStandard = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDiseases = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoverRestricciones = new System.Windows.Forms.Button();
            this.btnNewRestriction = new System.Windows.Forms.Button();
            this.btnAddRestriction = new System.Windows.Forms.Button();
            this.grdDataRestrictions = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.contextMenuRestriction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRemoveRestriction = new System.Windows.Forms.ToolStripMenuItem();
            this.uvRestriction = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRemoverRecomedaciones = new System.Windows.Forms.Button();
            this.btnNewRecommendation = new System.Windows.Forms.Button();
            this.grdDataRecommendation = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.contextMenuRecommendation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRemoveRecommendation = new System.Windows.Forms.ToolStripMenuItem();
            this.uvRecommendation = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.ddlSexTypeId = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.uvMedicalExamFiedValues)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataRestrictions)).BeginInit();
            this.contextMenuRestriction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uvRestriction)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataRecommendation)).BeginInit();
            this.contextMenuRecommendation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uvRecommendation)).BeginInit();
            this.SuspendLayout();
            // 
            // Nombre
            // 
            this.Nombre.AutoSize = true;
            this.Nombre.Location = new System.Drawing.Point(32, 33);
            this.Nombre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(80, 13);
            this.Nombre.TabIndex = 0;
            this.Nombre.Text = "Valor Analizar 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 92);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Operador";
            // 
            // ddlOperatorId
            // 
            this.ddlOperatorId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlOperatorId.FormattingEnabled = true;
            this.ddlOperatorId.Location = new System.Drawing.Point(121, 89);
            this.ddlOperatorId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlOperatorId.Name = "ddlOperatorId";
            this.ddlOperatorId.Size = new System.Drawing.Size(206, 21);
            this.ddlOperatorId.TabIndex = 3;
            this.uvMedicalExamFiedValues.GetValidationSettings(this.ddlOperatorId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvMedicalExamFiedValues.GetValidationSettings(this.ddlOperatorId).DataType = typeof(string);
            this.uvMedicalExamFiedValues.GetValidationSettings(this.ddlOperatorId).IsRequired = true;
            // 
            // ddlIsAnormal
            // 
            this.ddlIsAnormal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlIsAnormal.FormattingEnabled = true;
            this.ddlIsAnormal.Location = new System.Drawing.Point(121, 119);
            this.ddlIsAnormal.Margin = new System.Windows.Forms.Padding(2);
            this.ddlIsAnormal.Name = "ddlIsAnormal";
            this.ddlIsAnormal.Size = new System.Drawing.Size(206, 21);
            this.ddlIsAnormal.TabIndex = 4;
            this.uvMedicalExamFiedValues.GetValidationSettings(this.ddlIsAnormal).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvMedicalExamFiedValues.GetValidationSettings(this.ddlIsAnormal).DataType = typeof(string);
            this.uvMedicalExamFiedValues.GetValidationSettings(this.ddlIsAnormal).IsRequired = true;
            this.ddlIsAnormal.SelectedIndexChanged += new System.EventHandler(this.ddlIsAnormal_SelectedIndexChanged);
            // 
            // txtDiagnostic
            // 
            this.txtDiagnostic.Location = new System.Drawing.Point(121, 150);
            this.txtDiagnostic.Margin = new System.Windows.Forms.Padding(2);
            this.txtDiagnostic.Multiline = true;
            this.txtDiagnostic.Name = "txtDiagnostic";
            this.txtDiagnostic.ReadOnly = true;
            this.txtDiagnostic.Size = new System.Drawing.Size(206, 75);
            this.txtDiagnostic.TabIndex = 5;
            this.uvMedicalExamFiedValues.GetValidationSettings(this.txtDiagnostic).DataType = typeof(string);
            // 
            // txtAnalyzeValue1
            // 
            this.txtAnalyzeValue1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAnalyzeValue1.Location = new System.Drawing.Point(121, 30);
            this.txtAnalyzeValue1.Margin = new System.Windows.Forms.Padding(2);
            this.txtAnalyzeValue1.MaxLength = 7;
            this.txtAnalyzeValue1.Name = "txtAnalyzeValue1";
            this.txtAnalyzeValue1.Size = new System.Drawing.Size(206, 20);
            this.txtAnalyzeValue1.TabIndex = 47;
            this.txtAnalyzeValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uvMedicalExamFiedValues.GetValidationSettings(this.txtAnalyzeValue1).DataType = typeof(string);
            this.uvMedicalExamFiedValues.GetValidationSettings(this.txtAnalyzeValue1).IsRequired = true;
            // 
            // txtAnalyzeValue2
            // 
            this.txtAnalyzeValue2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAnalyzeValue2.Location = new System.Drawing.Point(121, 57);
            this.txtAnalyzeValue2.Margin = new System.Windows.Forms.Padding(2);
            this.txtAnalyzeValue2.MaxLength = 7;
            this.txtAnalyzeValue2.Name = "txtAnalyzeValue2";
            this.txtAnalyzeValue2.Size = new System.Drawing.Size(206, 20);
            this.txtAnalyzeValue2.TabIndex = 48;
            this.txtAnalyzeValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uvMedicalExamFiedValues.GetValidationSettings(this.txtAnalyzeValue2).DataType = typeof(string);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Valor Analizar 2";
            // 
            // txtLegalStandard
            // 
            this.txtLegalStandard.Location = new System.Drawing.Point(121, 267);
            this.txtLegalStandard.Margin = new System.Windows.Forms.Padding(2);
            this.txtLegalStandard.MaxLength = 250;
            this.txtLegalStandard.Multiline = true;
            this.txtLegalStandard.Name = "txtLegalStandard";
            this.txtLegalStandard.Size = new System.Drawing.Size(206, 162);
            this.txtLegalStandard.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 267);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Norma Legal";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 122);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Es Anormal";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 153);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Diagnóstico";
            // 
            // btnDiseases
            // 
            this.btnDiseases.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiseases.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_search;
            this.btnDiseases.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDiseases.Location = new System.Drawing.Point(330, 147);
            this.btnDiseases.Margin = new System.Windows.Forms.Padding(2);
            this.btnDiseases.Name = "btnDiseases";
            this.btnDiseases.Size = new System.Drawing.Size(26, 24);
            this.btnDiseases.TabIndex = 6;
            this.btnDiseases.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDiseases.UseVisualStyleBackColor = true;
            this.btnDiseases.Click += new System.EventHandler(this.btnDiseases_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnRemoverRestricciones);
            this.groupBox1.Controls.Add(this.btnNewRestriction);
            this.groupBox1.Controls.Add(this.btnAddRestriction);
            this.groupBox1.Controls.Add(this.grdDataRestrictions);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox1.Location = new System.Drawing.Point(360, 222);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(647, 207);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Restricciones";
            // 
            // btnRemoverRestricciones
            // 
            this.btnRemoverRestricciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoverRestricciones.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemoverRestricciones.Enabled = false;
            this.btnRemoverRestricciones.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRemoverRestricciones.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRemoverRestricciones.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRemoverRestricciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoverRestricciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverRestricciones.ForeColor = System.Drawing.Color.Black;
            this.btnRemoverRestricciones.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.btnRemoverRestricciones.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoverRestricciones.Location = new System.Drawing.Point(554, 62);
            this.btnRemoverRestricciones.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoverRestricciones.Name = "btnRemoverRestricciones";
            this.btnRemoverRestricciones.Size = new System.Drawing.Size(80, 24);
            this.btnRemoverRestricciones.TabIndex = 95;
            this.btnRemoverRestricciones.Text = "     Eliminar";
            this.btnRemoverRestricciones.UseVisualStyleBackColor = false;
            this.btnRemoverRestricciones.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnNewRestriction
            // 
            this.btnNewRestriction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewRestriction.BackColor = System.Drawing.SystemColors.Control;
            this.btnNewRestriction.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnNewRestriction.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnNewRestriction.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnNewRestriction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewRestriction.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewRestriction.ForeColor = System.Drawing.Color.Black;
            this.btnNewRestriction.Image = global::Sigesoft.Node.WinClient.UI.Resources.add;
            this.btnNewRestriction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewRestriction.Location = new System.Drawing.Point(554, 34);
            this.btnNewRestriction.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewRestriction.Name = "btnNewRestriction";
            this.btnNewRestriction.Size = new System.Drawing.Size(80, 24);
            this.btnNewRestriction.TabIndex = 94;
            this.btnNewRestriction.Text = "      Agregar";
            this.btnNewRestriction.UseVisualStyleBackColor = false;
            this.btnNewRestriction.Click += new System.EventHandler(this.btnNewRestriction_Click);
            // 
            // btnAddRestriction
            // 
            this.btnAddRestriction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRestriction.Image = global::Sigesoft.Node.WinClient.UI.Resources.add;
            this.btnAddRestriction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRestriction.Location = new System.Drawing.Point(577, -92);
            this.btnAddRestriction.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddRestriction.Name = "btnAddRestriction";
            this.btnAddRestriction.Size = new System.Drawing.Size(65, 30);
            this.btnAddRestriction.TabIndex = 34;
            this.btnAddRestriction.Text = "Agregar";
            this.btnAddRestriction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddRestriction.UseVisualStyleBackColor = true;
            // 
            // grdDataRestrictions
            // 
            this.grdDataRestrictions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataRestrictions.CausesValidation = false;
            this.grdDataRestrictions.ContextMenuStrip = this.contextMenuRestriction;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataRestrictions.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Restricción";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 572;
            ultraGridColumn4.Header.Caption = "Usuario Crea.";
            ultraGridColumn4.Header.VisiblePosition = 1;
            ultraGridColumn4.Width = 125;
            ultraGridColumn5.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn5.Header.Caption = "Fecha Crea.";
            ultraGridColumn5.Header.VisiblePosition = 2;
            ultraGridColumn5.Width = 150;
            ultraGridColumn6.Header.Caption = "Usuario Act.";
            ultraGridColumn6.Header.VisiblePosition = 3;
            ultraGridColumn6.Width = 125;
            ultraGridColumn7.Header.Caption = "Fecha Act.";
            ultraGridColumn7.Header.VisiblePosition = 4;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7});
            this.grdDataRestrictions.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataRestrictions.DisplayLayout.InterBandSpacing = 10;
            this.grdDataRestrictions.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataRestrictions.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataRestrictions.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataRestrictions.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataRestrictions.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataRestrictions.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataRestrictions.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataRestrictions.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataRestrictions.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDataRestrictions.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataRestrictions.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDataRestrictions.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataRestrictions.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDataRestrictions.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataRestrictions.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataRestrictions.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDataRestrictions.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdDataRestrictions.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDataRestrictions.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataRestrictions.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataRestrictions.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataRestrictions.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataRestrictions.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataRestrictions.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataRestrictions.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataRestrictions.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataRestrictions.Location = new System.Drawing.Point(20, 35);
            this.grdDataRestrictions.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataRestrictions.Name = "grdDataRestrictions";
            this.grdDataRestrictions.Size = new System.Drawing.Size(525, 167);
            this.grdDataRestrictions.TabIndex = 45;
            this.grdDataRestrictions.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdDataRestrictions_InitializeLayout);
            this.grdDataRestrictions.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdDataRestrictions_AfterSelectChange);
            this.grdDataRestrictions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdDataRestrictions_MouseDown);
            // 
            // contextMenuRestriction
            // 
            this.contextMenuRestriction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemoveRestriction});
            this.contextMenuRestriction.Name = "contextMenuStrip1";
            this.contextMenuRestriction.Size = new System.Drawing.Size(122, 26);
            // 
            // mnuRemoveRestriction
            // 
            this.mnuRemoveRestriction.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.mnuRemoveRestriction.Name = "mnuRemoveRestriction";
            this.mnuRemoveRestriction.Size = new System.Drawing.Size(121, 22);
            this.mnuRemoveRestriction.Text = "Remover";
            this.mnuRemoveRestriction.Click += new System.EventHandler(this.mnuRemoveRestriction_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnRemoverRecomedaciones);
            this.groupBox2.Controls.Add(this.btnNewRecommendation);
            this.groupBox2.Controls.Add(this.grdDataRecommendation);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox2.Location = new System.Drawing.Point(361, 30);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(647, 181);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Recomendaciones";
            // 
            // btnRemoverRecomedaciones
            // 
            this.btnRemoverRecomedaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoverRecomedaciones.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemoverRecomedaciones.Enabled = false;
            this.btnRemoverRecomedaciones.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRemoverRecomedaciones.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRemoverRecomedaciones.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRemoverRecomedaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoverRecomedaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverRecomedaciones.ForeColor = System.Drawing.Color.Black;
            this.btnRemoverRecomedaciones.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.btnRemoverRecomedaciones.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoverRecomedaciones.Location = new System.Drawing.Point(553, 45);
            this.btnRemoverRecomedaciones.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoverRecomedaciones.Name = "btnRemoverRecomedaciones";
            this.btnRemoverRecomedaciones.Size = new System.Drawing.Size(80, 24);
            this.btnRemoverRecomedaciones.TabIndex = 95;
            this.btnRemoverRecomedaciones.Text = "     Eliminar";
            this.btnRemoverRecomedaciones.UseVisualStyleBackColor = false;
            this.btnRemoverRecomedaciones.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnNewRecommendation
            // 
            this.btnNewRecommendation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewRecommendation.BackColor = System.Drawing.SystemColors.Control;
            this.btnNewRecommendation.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnNewRecommendation.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnNewRecommendation.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnNewRecommendation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewRecommendation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewRecommendation.ForeColor = System.Drawing.Color.Black;
            this.btnNewRecommendation.Image = global::Sigesoft.Node.WinClient.UI.Resources.add;
            this.btnNewRecommendation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewRecommendation.Location = new System.Drawing.Point(553, 17);
            this.btnNewRecommendation.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewRecommendation.Name = "btnNewRecommendation";
            this.btnNewRecommendation.Size = new System.Drawing.Size(80, 24);
            this.btnNewRecommendation.TabIndex = 94;
            this.btnNewRecommendation.Text = "      Agregar";
            this.btnNewRecommendation.UseVisualStyleBackColor = false;
            this.btnNewRecommendation.Click += new System.EventHandler(this.btnNewRecommendation_Click);
            // 
            // grdDataRecommendation
            // 
            this.grdDataRecommendation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataRecommendation.CausesValidation = false;
            this.grdDataRecommendation.ContextMenuStrip = this.contextMenuRecommendation;
            appearance8.BackColor = System.Drawing.Color.White;
            appearance8.BackColor2 = System.Drawing.Color.Silver;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataRecommendation.DisplayLayout.Appearance = appearance8;
            ultraGridColumn2.Header.Caption = "Recomendación";
            ultraGridColumn2.Header.VisiblePosition = 0;
            ultraGridColumn2.Width = 577;
            ultraGridColumn20.Header.Caption = "Usuario Crea.";
            ultraGridColumn20.Header.VisiblePosition = 1;
            ultraGridColumn20.Width = 125;
            ultraGridColumn21.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn21.Header.Caption = "Fecha Crea.";
            ultraGridColumn21.Header.VisiblePosition = 2;
            ultraGridColumn21.Width = 150;
            ultraGridColumn22.Header.Caption = "Usuario Act.";
            ultraGridColumn22.Header.VisiblePosition = 3;
            ultraGridColumn22.Width = 125;
            ultraGridColumn3.Header.Caption = "Fecha Act.";
            ultraGridColumn3.Header.VisiblePosition = 4;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn2,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn3});
            this.grdDataRecommendation.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdDataRecommendation.DisplayLayout.InterBandSpacing = 10;
            this.grdDataRecommendation.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataRecommendation.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataRecommendation.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataRecommendation.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataRecommendation.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataRecommendation.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataRecommendation.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataRecommendation.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataRecommendation.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance9.BackColor = System.Drawing.Color.Transparent;
            this.grdDataRecommendation.DisplayLayout.Override.CardAreaAppearance = appearance9;
            appearance10.BackColor = System.Drawing.Color.White;
            appearance10.BackColor2 = System.Drawing.Color.White;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataRecommendation.DisplayLayout.Override.CellAppearance = appearance10;
            this.grdDataRecommendation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.Color.White;
            appearance11.BackColor2 = System.Drawing.Color.LightGray;
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance11.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataRecommendation.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.grdDataRecommendation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance12.AlphaLevel = ((short)(187));
            appearance12.BackColor = System.Drawing.Color.Gainsboro;
            appearance12.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance12.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataRecommendation.DisplayLayout.Override.RowAlternateAppearance = appearance12;
            appearance13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataRecommendation.DisplayLayout.Override.RowSelectorAppearance = appearance13;
            this.grdDataRecommendation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance14.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance14.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance14.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance14.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance14.FontData.BoldAsString = "False";
            appearance14.ForeColor = System.Drawing.Color.Black;
            this.grdDataRecommendation.DisplayLayout.Override.SelectedRowAppearance = appearance14;
            this.grdDataRecommendation.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataRecommendation.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataRecommendation.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataRecommendation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataRecommendation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataRecommendation.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataRecommendation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataRecommendation.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataRecommendation.Location = new System.Drawing.Point(20, 17);
            this.grdDataRecommendation.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataRecommendation.Name = "grdDataRecommendation";
            this.grdDataRecommendation.Size = new System.Drawing.Size(525, 159);
            this.grdDataRecommendation.TabIndex = 45;
            this.grdDataRecommendation.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdDataRecommendation_AfterSelectChange);
            this.grdDataRecommendation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdDataRecommendation_MouseDown);
            // 
            // contextMenuRecommendation
            // 
            this.contextMenuRecommendation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemoveRecommendation});
            this.contextMenuRecommendation.Name = "contextMenuStrip1";
            this.contextMenuRecommendation.Size = new System.Drawing.Size(118, 26);
            // 
            // mnuRemoveRecommendation
            // 
            this.mnuRemoveRecommendation.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.mnuRemoveRecommendation.Name = "mnuRemoveRecommendation";
            this.mnuRemoveRecommendation.Size = new System.Drawing.Size(117, 22);
            this.mnuRemoveRecommendation.Text = "Eliminar";
            this.mnuRemoveRecommendation.Click += new System.EventHandler(this.mnuRemoveRecommendation_Click);
            // 
            // ddlSexTypeId
            // 
            this.ddlSexTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSexTypeId.FormattingEnabled = true;
            this.ddlSexTypeId.Location = new System.Drawing.Point(121, 237);
            this.ddlSexTypeId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlSexTypeId.Name = "ddlSexTypeId";
            this.ddlSexTypeId.Size = new System.Drawing.Size(137, 21);
            this.ddlSexTypeId.TabIndex = 49;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 240);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "Género";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(933, 452);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 101;
            this.btnCancel.Text = "   Salir";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(854, 452);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 100;
            this.btnOK.Text = "      Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmMedicalExamFieldValueEdicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1032, 487);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ddlSexTypeId);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtAnalyzeValue2);
            this.Controls.Add(this.txtAnalyzeValue1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDiseases);
            this.Controls.Add(this.txtDiagnostic);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ddlIsAnormal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtLegalStandard);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddlOperatorId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Nombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMedicalExamFieldValueEdicion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Valores de Campo ";
            this.Load += new System.EventHandler(this.frmMedicalExamEdicion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uvMedicalExamFiedValues)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataRestrictions)).EndInit();
            this.contextMenuRestriction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uvRestriction)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataRecommendation)).EndInit();
            this.contextMenuRecommendation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uvRecommendation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Nombre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ddlOperatorId;
        private Infragistics.Win.Misc.UltraValidator uvMedicalExamFiedValues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLegalStandard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ddlIsAnormal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDiagnostic;
        private System.Windows.Forms.Button btnDiseases;
        private System.Windows.Forms.GroupBox groupBox1;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataRestrictions;
        private System.Windows.Forms.Button btnAddRestriction;
        private System.Windows.Forms.ContextMenuStrip contextMenuRestriction;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveRestriction;
        private Infragistics.Win.Misc.UltraValidator uvRestriction;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataRecommendation;
        private System.Windows.Forms.ContextMenuStrip contextMenuRecommendation;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveRecommendation;
        private Infragistics.Win.Misc.UltraValidator uvRecommendation;
        private System.Windows.Forms.TextBox txtAnalyzeValue1;
        private System.Windows.Forms.TextBox txtAnalyzeValue2;
        private System.Windows.Forms.ComboBox ddlSexTypeId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnRemoverRestricciones;
        private System.Windows.Forms.Button btnNewRestriction;
        private System.Windows.Forms.Button btnRemoverRecomedaciones;
        private System.Windows.Forms.Button btnNewRecommendation;
    }
}