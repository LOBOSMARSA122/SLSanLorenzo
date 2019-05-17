namespace Sigesoft.Node.WinClient.UI
{
    partial class AdditionalExam
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CategoryName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_CategoryId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Componentes");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Componentes", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceComponentId");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdditionalExam));
            this.gdDataExams = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.rbNombreSubCategoria = new System.Windows.Forms.RadioButton();
            this.rbNombreComponente = new System.Windows.Forms.RadioButton();
            this.rbNombreCategoria = new System.Windows.Forms.RadioButton();
            this.rbPorCodigoSegus = new System.Windows.Forms.RadioButton();
            this.gbExamenesSeleccionados = new System.Windows.Forms.GroupBox();
            this.lvExamenesSeleccionados = new System.Windows.Forms.ListView();
            this.chExamen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMedicalExamId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chServiceComponentConcatId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtCommentary = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnRemoverExamenAuxiliar = new System.Windows.Forms.Button();
            this.btnAgregarExamenAuxiliar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gdDataExams)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbExamenesSeleccionados.SuspendLayout();
            this.SuspendLayout();
            // 
            // gdDataExams
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.gdDataExams.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Categoría";
            ultraGridColumn1.Header.Fixed = true;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 531;
            ultraGridColumn3.Header.Fixed = true;
            ultraGridColumn3.Header.VisiblePosition = 1;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.Fixed = true;
            ultraGridColumn4.Header.VisiblePosition = 2;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 3;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.Header.VisiblePosition = 4;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
            ultraGridColumn2.Header.VisiblePosition = 0;
            ultraGridColumn2.Width = 512;
            ultraGridColumn14.Header.VisiblePosition = 1;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.Header.VisiblePosition = 2;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn2,
            ultraGridColumn14,
            ultraGridColumn15});
            this.gdDataExams.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.gdDataExams.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.gdDataExams.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.gdDataExams.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gdDataExams.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.gdDataExams.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gdDataExams.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.gdDataExams.DisplayLayout.MaxColScrollRegions = 1;
            this.gdDataExams.DisplayLayout.MaxRowScrollRegions = 1;
            this.gdDataExams.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gdDataExams.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.gdDataExams.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.gdDataExams.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.gdDataExams.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.gdDataExams.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.gdDataExams.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.gdDataExams.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.gdDataExams.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.gdDataExams.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.gdDataExams.DisplayLayout.Override.CellAppearance = appearance8;
            this.gdDataExams.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.gdDataExams.DisplayLayout.Override.CellPadding = 0;
            this.gdDataExams.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            this.gdDataExams.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.gdDataExams.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.gdDataExams.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.gdDataExams.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.gdDataExams.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.gdDataExams.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.gdDataExams.DisplayLayout.Override.RowAppearance = appearance11;
            this.gdDataExams.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.gdDataExams.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gdDataExams.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.gdDataExams.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gdDataExams.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.gdDataExams.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.gdDataExams.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.gdDataExams.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gdDataExams.Location = new System.Drawing.Point(12, 72);
            this.gdDataExams.Name = "gdDataExams";
            this.gdDataExams.Size = new System.Drawing.Size(568, 390);
            this.gdDataExams.TabIndex = 107;
            this.gdDataExams.Text = "ultraGrid1";
            this.gdDataExams.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.gdDataExams_AfterSelectChange);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnFiltrar);
            this.groupBox1.Controls.Add(this.txtFiltro);
            this.groupBox1.Controls.Add(this.rbNombreSubCategoria);
            this.groupBox1.Controls.Add(this.rbNombreComponente);
            this.groupBox1.Controls.Add(this.rbNombreCategoria);
            this.groupBox1.Controls.Add(this.rbPorCodigoSegus);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(920, 54);
            this.groupBox1.TabIndex = 132;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_search;
            this.btnFiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.Location = new System.Drawing.Point(819, 17);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrar.TabIndex = 4;
            this.btnFiltrar.Text = "Buscar";
            this.btnFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(536, 17);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(256, 20);
            this.txtFiltro.TabIndex = 3;
            this.txtFiltro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltro_KeyPress);
            // 
            // rbNombreSubCategoria
            // 
            this.rbNombreSubCategoria.AutoSize = true;
            this.rbNombreSubCategoria.Location = new System.Drawing.Point(127, 20);
            this.rbNombreSubCategoria.Name = "rbNombreSubCategoria";
            this.rbNombreSubCategoria.Size = new System.Drawing.Size(129, 17);
            this.rbNombreSubCategoria.TabIndex = 2;
            this.rbNombreSubCategoria.Text = "Nombre SubCategoria";
            this.rbNombreSubCategoria.UseVisualStyleBackColor = true;
            // 
            // rbNombreComponente
            // 
            this.rbNombreComponente.AutoSize = true;
            this.rbNombreComponente.Location = new System.Drawing.Point(262, 20);
            this.rbNombreComponente.Name = "rbNombreComponente";
            this.rbNombreComponente.Size = new System.Drawing.Size(125, 17);
            this.rbNombreComponente.TabIndex = 1;
            this.rbNombreComponente.Text = "Nombre Componente";
            this.rbNombreComponente.UseVisualStyleBackColor = true;
            // 
            // rbNombreCategoria
            // 
            this.rbNombreCategoria.AutoSize = true;
            this.rbNombreCategoria.Checked = true;
            this.rbNombreCategoria.Location = new System.Drawing.Point(11, 20);
            this.rbNombreCategoria.Name = "rbNombreCategoria";
            this.rbNombreCategoria.Size = new System.Drawing.Size(110, 17);
            this.rbNombreCategoria.TabIndex = 0;
            this.rbNombreCategoria.TabStop = true;
            this.rbNombreCategoria.Text = "Nombre Categoria";
            this.rbNombreCategoria.UseVisualStyleBackColor = true;
            // 
            // rbPorCodigoSegus
            // 
            this.rbPorCodigoSegus.AutoSize = true;
            this.rbPorCodigoSegus.Location = new System.Drawing.Point(393, 20);
            this.rbPorCodigoSegus.Name = "rbPorCodigoSegus";
            this.rbPorCodigoSegus.Size = new System.Drawing.Size(91, 17);
            this.rbPorCodigoSegus.TabIndex = 0;
            this.rbPorCodigoSegus.Text = "Codigo Segus";
            this.rbPorCodigoSegus.UseVisualStyleBackColor = true;
            // 
            // gbExamenesSeleccionados
            // 
            this.gbExamenesSeleccionados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExamenesSeleccionados.Controls.Add(this.lvExamenesSeleccionados);
            this.gbExamenesSeleccionados.Location = new System.Drawing.Point(615, 72);
            this.gbExamenesSeleccionados.Name = "gbExamenesSeleccionados";
            this.gbExamenesSeleccionados.Size = new System.Drawing.Size(337, 247);
            this.gbExamenesSeleccionados.TabIndex = 133;
            this.gbExamenesSeleccionados.TabStop = false;
            this.gbExamenesSeleccionados.Text = "Examenes seleccionados";
            // 
            // lvExamenesSeleccionados
            // 
            this.lvExamenesSeleccionados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvExamenesSeleccionados.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chExamen,
            this.chMedicalExamId,
            this.chServiceComponentConcatId});
            this.lvExamenesSeleccionados.FullRowSelect = true;
            this.lvExamenesSeleccionados.Location = new System.Drawing.Point(7, 17);
            this.lvExamenesSeleccionados.Name = "lvExamenesSeleccionados";
            this.lvExamenesSeleccionados.Size = new System.Drawing.Size(324, 224);
            this.lvExamenesSeleccionados.TabIndex = 0;
            this.lvExamenesSeleccionados.UseCompatibleStateImageBehavior = false;
            this.lvExamenesSeleccionados.View = System.Windows.Forms.View.Details;
            this.lvExamenesSeleccionados.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvExamenesSeleccionados_ItemSelectionChanged);
            // 
            // chExamen
            // 
            this.chExamen.Text = "Examen";
            this.chExamen.Width = 272;
            // 
            // chMedicalExamId
            // 
            this.chMedicalExamId.Text = "MedicalExamId";
            this.chMedicalExamId.Width = 0;
            // 
            // chServiceComponentConcatId
            // 
            this.chServiceComponentConcatId.Text = "ServiceComponentConcatId";
            this.chServiceComponentConcatId.Width = 0;
            // 
            // txtCommentary
            // 
            this.txtCommentary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommentary.Location = new System.Drawing.Point(615, 352);
            this.txtCommentary.Multiline = true;
            this.txtCommentary.Name = "txtCommentary";
            this.txtCommentary.Size = new System.Drawing.Size(337, 72);
            this.txtCommentary.TabIndex = 134;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(615, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 135;
            this.label1.Text = "Comentario";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(877, 438);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 24);
            this.btnCancelar.TabIndex = 137;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(798, 438);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 136;
            this.btnOK.Text = "      Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnRemoverExamenAuxiliar
            // 
            this.btnRemoverExamenAuxiliar.Enabled = false;
            this.btnRemoverExamenAuxiliar.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoverExamenAuxiliar.Image")));
            this.btnRemoverExamenAuxiliar.Location = new System.Drawing.Point(585, 218);
            this.btnRemoverExamenAuxiliar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoverExamenAuxiliar.Name = "btnRemoverExamenAuxiliar";
            this.btnRemoverExamenAuxiliar.Size = new System.Drawing.Size(25, 22);
            this.btnRemoverExamenAuxiliar.TabIndex = 139;
            this.btnRemoverExamenAuxiliar.UseVisualStyleBackColor = true;
            this.btnRemoverExamenAuxiliar.Click += new System.EventHandler(this.btnRemoverExamenAuxiliar_Click);
            // 
            // btnAgregarExamenAuxiliar
            // 
            this.btnAgregarExamenAuxiliar.Enabled = false;
            this.btnAgregarExamenAuxiliar.Image = global::Sigesoft.Node.WinClient.UI.Resources.play_green;
            this.btnAgregarExamenAuxiliar.Location = new System.Drawing.Point(585, 177);
            this.btnAgregarExamenAuxiliar.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarExamenAuxiliar.Name = "btnAgregarExamenAuxiliar";
            this.btnAgregarExamenAuxiliar.Size = new System.Drawing.Size(25, 22);
            this.btnAgregarExamenAuxiliar.TabIndex = 138;
            this.btnAgregarExamenAuxiliar.UseVisualStyleBackColor = true;
            this.btnAgregarExamenAuxiliar.Click += new System.EventHandler(this.btnAgregarExamenAuxiliar_Click);
            // 
            // AdditionalExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 474);
            this.Controls.Add(this.btnRemoverExamenAuxiliar);
            this.Controls.Add(this.btnAgregarExamenAuxiliar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCommentary);
            this.Controls.Add(this.gbExamenesSeleccionados);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gdDataExams);
            this.Name = "AdditionalExam";
            this.Text = "Agregar Examen";
            this.Load += new System.EventHandler(this.AdditionalExam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gdDataExams)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbExamenesSeleccionados.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid gdDataExams;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.RadioButton rbNombreSubCategoria;
        private System.Windows.Forms.RadioButton rbNombreComponente;
        private System.Windows.Forms.RadioButton rbNombreCategoria;
        private System.Windows.Forms.RadioButton rbPorCodigoSegus;
        private System.Windows.Forms.GroupBox gbExamenesSeleccionados;
        private System.Windows.Forms.ListView lvExamenesSeleccionados;
        private System.Windows.Forms.ColumnHeader chExamen;
        private System.Windows.Forms.ColumnHeader chMedicalExamId;
        private System.Windows.Forms.ColumnHeader chServiceComponentConcatId;
        private System.Windows.Forms.TextBox txtCommentary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnRemoverExamenAuxiliar;
        private System.Windows.Forms.Button btnAgregarExamenAuxiliar;
    }
}