namespace Sigesoft.Node.WinClient.UI
{
    partial class FrmAddPlain
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CategoryName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_CategoryId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Componentes");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Componentes", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceComponentId");
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
            this.GridListaUnidadProductiva = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.gbExamenesSeleccionados = new System.Windows.Forms.GroupBox();
            this.lvPlanes = new System.Windows.Forms.ListView();
            this.chExamen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMedicalExamId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chServiceComponentConcatId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemoverPlan = new System.Windows.Forms.Button();
            this.btnAgregarPlan = new System.Windows.Forms.Button();
            this.btnGrabarPlan = new System.Windows.Forms.Button();
            this.btnCerrarAddPlan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridListaUnidadProductiva)).BeginInit();
            this.gbExamenesSeleccionados.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridListaUnidadProductiva
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.GridListaUnidadProductiva.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Categoría";
            ultraGridColumn1.Header.Fixed = true;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 249;
            ultraGridColumn2.Header.Fixed = true;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.Fixed = true;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5});
            ultraGridBand2.ColHeadersVisible = false;
            ultraGridColumn6.Header.Caption = "Componente";
            ultraGridColumn6.Header.VisiblePosition = 0;
            ultraGridColumn6.Width = 230;
            ultraGridColumn7.Header.VisiblePosition = 1;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.Header.VisiblePosition = 2;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8});
            this.GridListaUnidadProductiva.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.GridListaUnidadProductiva.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.GridListaUnidadProductiva.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.GridListaUnidadProductiva.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.GridListaUnidadProductiva.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.GridListaUnidadProductiva.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.GridListaUnidadProductiva.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.GridListaUnidadProductiva.DisplayLayout.MaxColScrollRegions = 1;
            this.GridListaUnidadProductiva.DisplayLayout.MaxRowScrollRegions = 1;
            this.GridListaUnidadProductiva.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GridListaUnidadProductiva.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.GridListaUnidadProductiva.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.GridListaUnidadProductiva.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.GridListaUnidadProductiva.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.GridListaUnidadProductiva.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.GridListaUnidadProductiva.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.GridListaUnidadProductiva.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.GridListaUnidadProductiva.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.GridListaUnidadProductiva.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.GridListaUnidadProductiva.DisplayLayout.Override.CellAppearance = appearance8;
            this.GridListaUnidadProductiva.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.GridListaUnidadProductiva.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.GridListaUnidadProductiva.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.GridListaUnidadProductiva.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.GridListaUnidadProductiva.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.GridListaUnidadProductiva.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.GridListaUnidadProductiva.DisplayLayout.Override.RowAppearance = appearance11;
            this.GridListaUnidadProductiva.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.GridListaUnidadProductiva.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.GridListaUnidadProductiva.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.GridListaUnidadProductiva.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.GridListaUnidadProductiva.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.GridListaUnidadProductiva.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.GridListaUnidadProductiva.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.GridListaUnidadProductiva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridListaUnidadProductiva.Location = new System.Drawing.Point(12, 12);
            this.GridListaUnidadProductiva.Name = "GridListaUnidadProductiva";
            this.GridListaUnidadProductiva.Size = new System.Drawing.Size(308, 331);
            this.GridListaUnidadProductiva.TabIndex = 106;
            this.GridListaUnidadProductiva.Text = "ultraGrid1";
            this.GridListaUnidadProductiva.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.ultraGrid1_AfterSelectChange);
            // 
            // gbExamenesSeleccionados
            // 
            this.gbExamenesSeleccionados.Controls.Add(this.lvPlanes);
            this.gbExamenesSeleccionados.Location = new System.Drawing.Point(355, 12);
            this.gbExamenesSeleccionados.Name = "gbExamenesSeleccionados";
            this.gbExamenesSeleccionados.Size = new System.Drawing.Size(289, 331);
            this.gbExamenesSeleccionados.TabIndex = 104;
            this.gbExamenesSeleccionados.TabStop = false;
            this.gbExamenesSeleccionados.Text = "Planes de descuento";
            // 
            // lvPlanes
            // 
            this.lvPlanes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chExamen,
            this.chMedicalExamId,
            this.chServiceComponentConcatId});
            this.lvPlanes.FullRowSelect = true;
            this.lvPlanes.Location = new System.Drawing.Point(7, 17);
            this.lvPlanes.Name = "lvPlanes";
            this.lvPlanes.Size = new System.Drawing.Size(276, 308);
            this.lvPlanes.TabIndex = 0;
            this.lvPlanes.UseCompatibleStateImageBehavior = false;
            this.lvPlanes.View = System.Windows.Forms.View.Details;
            this.lvPlanes.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvExamenesSeleccionados_ItemSelectionChanged);
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
            // btnRemoverPlan
            // 
            this.btnRemoverPlan.Enabled = false;
            this.btnRemoverPlan.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnRemoverPlan.Location = new System.Drawing.Point(325, 143);
            this.btnRemoverPlan.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoverPlan.Name = "btnRemoverPlan";
            this.btnRemoverPlan.Size = new System.Drawing.Size(25, 22);
            this.btnRemoverPlan.TabIndex = 103;
            this.btnRemoverPlan.UseVisualStyleBackColor = true;
            this.btnRemoverPlan.Click += new System.EventHandler(this.btnRemoverExamenAuxiliar_Click);
            // 
            // btnAgregarPlan
            // 
            this.btnAgregarPlan.Image = global::Sigesoft.Node.WinClient.UI.Resources.play_green;
            this.btnAgregarPlan.Location = new System.Drawing.Point(325, 102);
            this.btnAgregarPlan.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarPlan.Name = "btnAgregarPlan";
            this.btnAgregarPlan.Size = new System.Drawing.Size(25, 22);
            this.btnAgregarPlan.TabIndex = 102;
            this.btnAgregarPlan.UseVisualStyleBackColor = true;
            this.btnAgregarPlan.Click += new System.EventHandler(this.btnAgregarExamenAuxiliar_Click);
            // 
            // btnGrabarPlan
            // 
            this.btnGrabarPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGrabarPlan.BackColor = System.Drawing.SystemColors.Control;
            this.btnGrabarPlan.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGrabarPlan.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGrabarPlan.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGrabarPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrabarPlan.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnGrabarPlan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabarPlan.Location = new System.Drawing.Point(491, 350);
            this.btnGrabarPlan.Margin = new System.Windows.Forms.Padding(2);
            this.btnGrabarPlan.Name = "btnGrabarPlan";
            this.btnGrabarPlan.Size = new System.Drawing.Size(75, 24);
            this.btnGrabarPlan.TabIndex = 100;
            this.btnGrabarPlan.Text = "      Guardar";
            this.btnGrabarPlan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabarPlan.UseVisualStyleBackColor = false;
            this.btnGrabarPlan.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCerrarAddPlan
            // 
            this.btnCerrarAddPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCerrarAddPlan.BackColor = System.Drawing.SystemColors.Control;
            this.btnCerrarAddPlan.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrarAddPlan.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCerrarAddPlan.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCerrarAddPlan.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCerrarAddPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarAddPlan.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCerrarAddPlan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarAddPlan.Location = new System.Drawing.Point(570, 350);
            this.btnCerrarAddPlan.Margin = new System.Windows.Forms.Padding(2);
            this.btnCerrarAddPlan.Name = "btnCerrarAddPlan";
            this.btnCerrarAddPlan.Size = new System.Drawing.Size(75, 24);
            this.btnCerrarAddPlan.TabIndex = 101;
            this.btnCerrarAddPlan.Text = "Cancelar";
            this.btnCerrarAddPlan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrarAddPlan.UseVisualStyleBackColor = false;
            // 
            // FrmAddPlain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 385);
            this.Controls.Add(this.GridListaUnidadProductiva);
            this.Controls.Add(this.btnRemoverPlan);
            this.Controls.Add(this.gbExamenesSeleccionados);
            this.Controls.Add(this.btnAgregarPlan);
            this.Controls.Add(this.btnGrabarPlan);
            this.Controls.Add(this.btnCerrarAddPlan);
            this.Name = "FrmAddPlain";
            this.Text = "Agregar Plan";
            ((System.ComponentModel.ISupportInitialize)(this.GridListaUnidadProductiva)).EndInit();
            this.gbExamenesSeleccionados.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid GridListaUnidadProductiva;
        private System.Windows.Forms.Button btnAgregarPlan;
        private System.Windows.Forms.Button btnRemoverPlan;
        private System.Windows.Forms.GroupBox gbExamenesSeleccionados;
        private System.Windows.Forms.ListView lvPlanes;
        private System.Windows.Forms.ColumnHeader chExamen;
        private System.Windows.Forms.ColumnHeader chMedicalExamId;
        private System.Windows.Forms.ColumnHeader chServiceComponentConcatId;
        private System.Windows.Forms.Button btnGrabarPlan;
        private System.Windows.Forms.Button btnCerrarAddPlan;
    }
}