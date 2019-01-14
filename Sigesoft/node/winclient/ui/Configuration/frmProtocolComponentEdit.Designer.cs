namespace Sigesoft.Node.WinClient.UI.Configuration
{
    partial class frmProtocolComponentEdit
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_BasePrice");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_ComponentTypeId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CategoryName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.gbAddExam = new Infragistics.Win.Misc.UltraGroupBox();
            this.lblRecordCountMedicalExam = new System.Windows.Forms.Label();
            this.grdComponent = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblRecordCount1 = new System.Windows.Forms.Label();
            this.chkIsConditional = new System.Windows.Forms.CheckBox();
            this.gbConditional = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtMayorque = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.chkIMC = new System.Windows.Forms.CheckBox();
            this.txtAge = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.cbGender = new System.Windows.Forms.ComboBox();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.lblExamenSeleccionado = new System.Windows.Forms.Label();
            this.txtFinalPrice = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbGrupoEtario = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkExaAdd = new System.Windows.Forms.CheckBox();
            this.gbFilter = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtComponentName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gbAddExam)).BeginInit();
            this.gbAddExam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbConditional)).BeginInit();
            this.gbConditional.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMayorque)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).BeginInit();
            this.ultraGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinalPrice)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbFilter)).BeginInit();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAddExam
            // 
            this.gbAddExam.Controls.Add(this.lblRecordCountMedicalExam);
            this.gbAddExam.Controls.Add(this.grdComponent);
            this.gbAddExam.Controls.Add(this.lblRecordCount1);
            this.gbAddExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAddExam.ForeColor = System.Drawing.Color.MediumBlue;
            this.gbAddExam.Location = new System.Drawing.Point(25, 95);
            this.gbAddExam.Name = "gbAddExam";
            this.gbAddExam.Size = new System.Drawing.Size(487, 254);
            this.gbAddExam.TabIndex = 22;
            this.gbAddExam.Text = "Selección de examen";
            this.gbAddExam.Click += new System.EventHandler(this.gbAddExam_Click);
            // 
            // lblRecordCountMedicalExam
            // 
            this.lblRecordCountMedicalExam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountMedicalExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountMedicalExam.Location = new System.Drawing.Point(473, -20);
            this.lblRecordCountMedicalExam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountMedicalExam.Name = "lblRecordCountMedicalExam";
            this.lblRecordCountMedicalExam.Size = new System.Drawing.Size(10, 10);
            this.lblRecordCountMedicalExam.TabIndex = 45;
            this.lblRecordCountMedicalExam.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountMedicalExam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdComponent
            // 
            this.grdComponent.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.LightGray;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdComponent.DisplayLayout.Appearance = appearance1;
            ultraGridColumn4.Header.VisiblePosition = 1;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.Caption = "Examen";
            ultraGridColumn5.Header.VisiblePosition = 0;
            ultraGridColumn5.Width = 258;
            ultraGridColumn6.Header.Caption = "Precio Base";
            ultraGridColumn6.Header.VisiblePosition = 3;
            ultraGridColumn6.Width = 80;
            ultraGridColumn2.Header.Caption = "Tipo";
            ultraGridColumn2.Header.VisiblePosition = 5;
            ultraGridColumn1.Header.VisiblePosition = 4;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn3.Header.Caption = "Categoria";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 110;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn2,
            ultraGridColumn1,
            ultraGridColumn3});
            this.grdComponent.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdComponent.DisplayLayout.InterBandSpacing = 10;
            this.grdComponent.DisplayLayout.MaxColScrollRegions = 1;
            this.grdComponent.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdComponent.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdComponent.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdComponent.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponent.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdComponent.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdComponent.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponent.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdComponent.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdComponent.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdComponent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdComponent.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdComponent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdComponent.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdComponent.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdComponent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdComponent.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdComponent.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdComponent.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdComponent.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdComponent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdComponent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdComponent.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdComponent.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdComponent.Location = new System.Drawing.Point(5, 20);
            this.grdComponent.Margin = new System.Windows.Forms.Padding(2);
            this.grdComponent.Name = "grdComponent";
            this.grdComponent.Size = new System.Drawing.Size(469, 229);
            this.grdComponent.TabIndex = 46;
            this.grdComponent.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdComponent_AfterSelectChange);
            // 
            // lblRecordCount1
            // 
            this.lblRecordCount1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount1.Location = new System.Drawing.Point(298, 0);
            this.lblRecordCount1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount1.Name = "lblRecordCount1";
            this.lblRecordCount1.Size = new System.Drawing.Size(183, 18);
            this.lblRecordCount1.TabIndex = 45;
            this.lblRecordCount1.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIsConditional
            // 
            this.chkIsConditional.AutoSize = true;
            this.chkIsConditional.ForeColor = System.Drawing.Color.Black;
            this.chkIsConditional.Location = new System.Drawing.Point(19, 143);
            this.chkIsConditional.Name = "chkIsConditional";
            this.chkIsConditional.Size = new System.Drawing.Size(151, 17);
            this.chkIsConditional.TabIndex = 49;
            this.chkIsConditional.Text = "Es un examen Condicional";
            this.chkIsConditional.UseVisualStyleBackColor = true;
            this.chkIsConditional.CheckedChanged += new System.EventHandler(this.chkIsConditional_CheckedChanged);
            // 
            // gbConditional
            // 
            this.gbConditional.Controls.Add(this.txtMayorque);
            this.gbConditional.Controls.Add(this.chkIMC);
            this.gbConditional.Controls.Add(this.txtAge);
            this.gbConditional.Controls.Add(this.cbGender);
            this.gbConditional.Controls.Add(this.cbOperator);
            this.gbConditional.Controls.Add(this.label4);
            this.gbConditional.Controls.Add(this.label6);
            this.gbConditional.Enabled = false;
            this.gbConditional.Location = new System.Drawing.Point(37, 166);
            this.gbConditional.Name = "gbConditional";
            this.gbConditional.Size = new System.Drawing.Size(277, 146);
            this.gbConditional.TabIndex = 48;
            this.gbConditional.Text = "Configurar Condicionales";
            // 
            // txtMayorque
            // 
            this.txtMayorque.Enabled = false;
            this.txtMayorque.Location = new System.Drawing.Point(181, 78);
            this.txtMayorque.Name = "txtMayorque";
            this.txtMayorque.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.txtMayorque.PromptChar = ' ';
            this.txtMayorque.Size = new System.Drawing.Size(73, 21);
            this.txtMayorque.TabIndex = 51;
            // 
            // chkIMC
            // 
            this.chkIMC.AutoSize = true;
            this.chkIMC.ForeColor = System.Drawing.Color.Black;
            this.chkIMC.Location = new System.Drawing.Point(27, 82);
            this.chkIMC.Name = "chkIMC";
            this.chkIMC.Size = new System.Drawing.Size(113, 17);
            this.chkIMC.TabIndex = 50;
            this.chkIMC.Text = "I.M.C   Mayor que:";
            this.chkIMC.UseVisualStyleBackColor = true;
            this.chkIMC.CheckedChanged += new System.EventHandler(this.chkIMC_CheckedChanged);
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(181, 28);
            this.txtAge.Name = "txtAge";
            this.txtAge.PromptChar = ' ';
            this.txtAge.Size = new System.Drawing.Size(73, 21);
            this.txtAge.TabIndex = 20;
            // 
            // cbGender
            // 
            this.cbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGender.FormattingEnabled = true;
            this.cbGender.Location = new System.Drawing.Point(75, 55);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(96, 21);
            this.cbGender.TabIndex = 19;
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(75, 28);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(96, 21);
            this.cbOperator.TabIndex = 17;
            this.cbOperator.SelectedIndexChanged += new System.EventHandler(this.cbOperator_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(24, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Género";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(19, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Edad";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ultraGroupBox3
            // 
            this.ultraGroupBox3.Controls.Add(this.lblExamenSeleccionado);
            this.ultraGroupBox3.Controls.Add(this.txtFinalPrice);
            this.ultraGroupBox3.Controls.Add(this.label1);
            this.ultraGroupBox3.Controls.Add(this.label2);
            this.ultraGroupBox3.Location = new System.Drawing.Point(16, 35);
            this.ultraGroupBox3.Name = "ultraGroupBox3";
            this.ultraGroupBox3.Size = new System.Drawing.Size(298, 82);
            this.ultraGroupBox3.TabIndex = 47;
            this.ultraGroupBox3.Text = "Datos de Examen";
            // 
            // lblExamenSeleccionado
            // 
            this.lblExamenSeleccionado.BackColor = System.Drawing.Color.Linen;
            this.lblExamenSeleccionado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblExamenSeleccionado.Location = new System.Drawing.Point(66, 25);
            this.lblExamenSeleccionado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExamenSeleccionado.Name = "lblExamenSeleccionado";
            this.lblExamenSeleccionado.Size = new System.Drawing.Size(218, 20);
            this.lblExamenSeleccionado.TabIndex = 54;
            // 
            // txtFinalPrice
            // 
            this.txtFinalPrice.Location = new System.Drawing.Point(66, 49);
            this.txtFinalPrice.Name = "txtFinalPrice";
            this.txtFinalPrice.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.txtFinalPrice.PromptChar = ' ';
            this.txtFinalPrice.Size = new System.Drawing.Size(91, 21);
            this.txtFinalPrice.TabIndex = 15;
            this.txtFinalPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFinalPrice_KeyPress);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(18, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Precio";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(18, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 26);
            this.label2.TabIndex = 52;
            this.label2.Text = "Examen";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbGrupoEtario);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chkExaAdd);
            this.groupBox2.Controls.Add(this.ultraGroupBox3);
            this.groupBox2.Controls.Add(this.chkIsConditional);
            this.groupBox2.Controls.Add(this.gbConditional);
            this.groupBox2.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox2.Location = new System.Drawing.Point(528, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 327);
            this.groupBox2.TabIndex = 53;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Examen seleccionado";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // cbGrupoEtario
            // 
            this.cbGrupoEtario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupoEtario.FormattingEnabled = true;
            this.cbGrupoEtario.Location = new System.Drawing.Point(112, 271);
            this.cbGrupoEtario.Name = "cbGrupoEtario";
            this.cbGrupoEtario.Size = new System.Drawing.Size(96, 21);
            this.cbGrupoEtario.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(56, 271);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 41);
            this.label3.TabIndex = 51;
            this.label3.Text = "Grupo Etario";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkExaAdd
            // 
            this.chkExaAdd.AutoSize = true;
            this.chkExaAdd.ForeColor = System.Drawing.Color.Black;
            this.chkExaAdd.Location = new System.Drawing.Point(19, 120);
            this.chkExaAdd.Name = "chkExaAdd";
            this.chkExaAdd.Size = new System.Drawing.Size(139, 17);
            this.chkExaAdd.TabIndex = 50;
            this.chkExaAdd.Text = "Es un examen Adicional";
            this.chkExaAdd.UseVisualStyleBackColor = true;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnFilter);
            this.gbFilter.Controls.Add(this.txtComponentName);
            this.gbFilter.Controls.Add(this.label5);
            this.gbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.ForeColor = System.Drawing.Color.MediumBlue;
            this.gbFilter.Location = new System.Drawing.Point(18, 17);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(488, 52);
            this.gbFilter.TabIndex = 21;
            this.gbFilter.Text = "Busqueda / Filtro";
            // 
            // txtComponentName
            // 
            this.txtComponentName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComponentName.Location = new System.Drawing.Point(71, 23);
            this.txtComponentName.MaxLength = 250;
            this.txtComponentName.Name = "txtComponentName";
            this.txtComponentName.Size = new System.Drawing.Size(328, 20);
            this.txtComponentName.TabIndex = 11;
            this.txtComponentName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtComponentName_KeyPress);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(13, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 22);
            this.label5.TabIndex = 12;
            this.label5.Text = "Examen";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(773, 354);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 101;
            this.btnCancel.Text = "   Salir";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(694, 354);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 100;
            this.btnOK.Text = "      Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.BackColor = System.Drawing.SystemColors.Control;
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFilter.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFilter.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Image = global::Sigesoft.Node.WinClient.UI.Resources.find;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(406, 22);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 22);
            this.btnFilter.TabIndex = 54;
            this.btnFilter.Text = "&Buscar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // frmProtocolComponentEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(860, 405);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbAddExam);
            this.ForeColor = System.Drawing.Color.MediumBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProtocolComponentEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Componentes de Protocolo";
            this.Load += new System.EventHandler(this.frmProtocolComponentEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbAddExam)).EndInit();
            this.gbAddExam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbConditional)).EndInit();
            this.gbConditional.ResumeLayout(false);
            this.gbConditional.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMayorque)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).EndInit();
            this.ultraGroupBox3.ResumeLayout(false);
            this.ultraGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinalPrice)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbFilter)).EndInit();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox gbAddExam;
        private System.Windows.Forms.Label lblRecordCount1;
        private System.Windows.Forms.CheckBox chkIsConditional;
        private Infragistics.Win.Misc.UltraGroupBox gbConditional;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor txtAge;
        private System.Windows.Forms.ComboBox cbGender;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox3;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor txtFinalPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRecordCountMedicalExam;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdComponent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblExamenSeleccionado;
        private Infragistics.Win.Misc.UltraGroupBox gbFilter;
        private System.Windows.Forms.TextBox txtComponentName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor txtMayorque;
        private System.Windows.Forms.CheckBox chkIMC;
        private System.Windows.Forms.CheckBox chkExaAdd;
        private System.Windows.Forms.ComboBox cbGrupoEtario;
        private System.Windows.Forms.Label label3;
    }
}