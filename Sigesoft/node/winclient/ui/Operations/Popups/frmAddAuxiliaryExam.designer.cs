namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    partial class frmAddAuxiliaryExam
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_MedicalExamId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CategoryName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.cbCategoria = new System.Windows.Forms.ComboBox();
            this.lblRecordCountExamenAuxiliar = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtExamen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grdExamenAuxilar = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnRemoverExamenAuxiliar = new System.Windows.Forms.Button();
            this.btnAgregarExamenAuxiliar = new System.Windows.Forms.Button();
            this.gbExamenesSeleccionados = new System.Windows.Forms.GroupBox();
            this.lvExamenesSeleccionados = new System.Windows.Forms.ListView();
            this.chExamen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCategoria = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chMedicalExamId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExamenAuxilar)).BeginInit();
            this.gbExamenesSeleccionados.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCategoria
            // 
            this.cbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategoria.FormattingEnabled = true;
            this.cbCategoria.Location = new System.Drawing.Point(64, 22);
            this.cbCategoria.Name = "cbCategoria";
            this.cbCategoria.Size = new System.Drawing.Size(285, 21);
            this.cbCategoria.TabIndex = 86;
            this.cbCategoria.SelectedIndexChanged += new System.EventHandler(this.cbCategoria_SelectedIndexChanged);
            // 
            // lblRecordCountExamenAuxiliar
            // 
            this.lblRecordCountExamenAuxiliar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecordCountExamenAuxiliar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountExamenAuxiliar.Location = new System.Drawing.Point(118, 337);
            this.lblRecordCountExamenAuxiliar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountExamenAuxiliar.Name = "lblRecordCountExamenAuxiliar";
            this.lblRecordCountExamenAuxiliar.Size = new System.Drawing.Size(226, 17);
            this.lblRecordCountExamenAuxiliar.TabIndex = 85;
            this.lblRecordCountExamenAuxiliar.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountExamenAuxiliar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 13);
            this.label16.TabIndex = 84;
            this.label16.Text = "Categoria";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.txtExamen);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.grdExamenAuxilar);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.lblRecordCountExamenAuxiliar);
            this.groupBox1.Controls.Add(this.cbCategoria);
            this.groupBox1.Location = new System.Drawing.Point(5, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 363);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Búsqueda de Examenes";
            // 
            // txtExamen
            // 
            this.txtExamen.Location = new System.Drawing.Point(64, 48);
            this.txtExamen.Name = "txtExamen";
            this.txtExamen.Size = new System.Drawing.Size(285, 20);
            this.txtExamen.TabIndex = 91;
            this.txtExamen.TextChanged += new System.EventHandler(this.txtExamen_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 90;
            this.label1.Text = "Nombre";
            // 
            // grdExamenAuxilar
            // 
            this.grdExamenAuxilar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdExamenAuxilar.CausesValidation = false;
            appearance1.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdExamenAuxilar.DisplayLayout.Appearance = appearance1;
            ultraGridColumn6.Header.VisiblePosition = 0;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn2.Header.Caption = "Examen";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 197;
            ultraGridColumn5.Header.Caption = "Categoría";
            ultraGridColumn5.Header.VisiblePosition = 2;
            ultraGridColumn5.Width = 120;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn6,
            ultraGridColumn2,
            ultraGridColumn5});
            this.grdExamenAuxilar.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdExamenAuxilar.DisplayLayout.InterBandSpacing = 10;
            this.grdExamenAuxilar.DisplayLayout.MaxColScrollRegions = 1;
            this.grdExamenAuxilar.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdExamenAuxilar.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdExamenAuxilar.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdExamenAuxilar.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdExamenAuxilar.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdExamenAuxilar.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdExamenAuxilar.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdExamenAuxilar.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdExamenAuxilar.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdExamenAuxilar.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.SystemColors.Control;
            appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdExamenAuxilar.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdExamenAuxilar.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            appearance5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdExamenAuxilar.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdExamenAuxilar.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdExamenAuxilar.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.FontData.BoldAsString = "True";
            this.grdExamenAuxilar.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdExamenAuxilar.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdExamenAuxilar.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdExamenAuxilar.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdExamenAuxilar.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdExamenAuxilar.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdExamenAuxilar.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdExamenAuxilar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdExamenAuxilar.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdExamenAuxilar.Location = new System.Drawing.Point(9, 72);
            this.grdExamenAuxilar.Margin = new System.Windows.Forms.Padding(2);
            this.grdExamenAuxilar.Name = "grdExamenAuxilar";
            this.grdExamenAuxilar.Size = new System.Drawing.Size(340, 267);
            this.grdExamenAuxilar.TabIndex = 89;
            this.grdExamenAuxilar.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdExamenAuxilar_AfterSelectChange);
            // 
            // btnRemoverExamenAuxiliar
            // 
            this.btnRemoverExamenAuxiliar.Enabled = false;
            this.btnRemoverExamenAuxiliar.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnRemoverExamenAuxiliar.Location = new System.Drawing.Point(365, 154);
            this.btnRemoverExamenAuxiliar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoverExamenAuxiliar.Name = "btnRemoverExamenAuxiliar";
            this.btnRemoverExamenAuxiliar.Size = new System.Drawing.Size(25, 22);
            this.btnRemoverExamenAuxiliar.TabIndex = 91;
            this.btnRemoverExamenAuxiliar.UseVisualStyleBackColor = true;
            this.btnRemoverExamenAuxiliar.Click += new System.EventHandler(this.btnRemoverExamenAuxiliar_Click);
            // 
            // btnAgregarExamenAuxiliar
            // 
            this.btnAgregarExamenAuxiliar.Image = global::Sigesoft.Node.WinClient.UI.Resources.play_green;
            this.btnAgregarExamenAuxiliar.Location = new System.Drawing.Point(365, 113);
            this.btnAgregarExamenAuxiliar.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarExamenAuxiliar.Name = "btnAgregarExamenAuxiliar";
            this.btnAgregarExamenAuxiliar.Size = new System.Drawing.Size(25, 22);
            this.btnAgregarExamenAuxiliar.TabIndex = 90;
            this.btnAgregarExamenAuxiliar.UseVisualStyleBackColor = true;
            this.btnAgregarExamenAuxiliar.Click += new System.EventHandler(this.btnAgregarExamenAuxiliar_Click);
            // 
            // gbExamenesSeleccionados
            // 
            this.gbExamenesSeleccionados.Controls.Add(this.lvExamenesSeleccionados);
            this.gbExamenesSeleccionados.Location = new System.Drawing.Point(394, 12);
            this.gbExamenesSeleccionados.Name = "gbExamenesSeleccionados";
            this.gbExamenesSeleccionados.Size = new System.Drawing.Size(308, 358);
            this.gbExamenesSeleccionados.TabIndex = 92;
            this.gbExamenesSeleccionados.TabStop = false;
            this.gbExamenesSeleccionados.Text = "Examenes seleccionados";
            // 
            // lvExamenesSeleccionados
            // 
            this.lvExamenesSeleccionados.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chExamen,
            this.chCategoria,
            this.chMedicalExamId});
            this.lvExamenesSeleccionados.FullRowSelect = true;
            this.lvExamenesSeleccionados.Location = new System.Drawing.Point(6, 17);
            this.lvExamenesSeleccionados.Name = "lvExamenesSeleccionados";
            this.lvExamenesSeleccionados.Size = new System.Drawing.Size(297, 332);
            this.lvExamenesSeleccionados.TabIndex = 0;
            this.lvExamenesSeleccionados.UseCompatibleStateImageBehavior = false;
            this.lvExamenesSeleccionados.View = System.Windows.Forms.View.Details;
            this.lvExamenesSeleccionados.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvExamenesSeleccionados_ItemSelectionChanged);
            // 
            // chExamen
            // 
            this.chExamen.Text = "Examen";
            this.chExamen.Width = 191;
            // 
            // chCategoria
            // 
            this.chCategoria.Text = "Categoria";
            this.chCategoria.Width = 102;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(625, 374);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 94;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(541, 374);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 93;
            this.btnOK.Text = "Aceptar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chMedicalExamId
            // 
            this.chMedicalExamId.Text = "MedicalExamId";
            this.chMedicalExamId.Width = 0;
            // 
            // frmAddAuxiliaryExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 408);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbExamenesSeleccionados);
            this.Controls.Add(this.btnRemoverExamenAuxiliar);
            this.Controls.Add(this.btnAgregarExamenAuxiliar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddAuxiliaryExam";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Examenes Auxiliares";
            this.Load += new System.EventHandler(this.frmAddAuxiliaryExam_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExamenAuxilar)).EndInit();
            this.gbExamenesSeleccionados.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCategoria;
        private System.Windows.Forms.Label lblRecordCountExamenAuxiliar;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoverExamenAuxiliar;
        private System.Windows.Forms.Button btnAgregarExamenAuxiliar;
        private System.Windows.Forms.GroupBox gbExamenesSeleccionados;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdExamenAuxilar;
        private System.Windows.Forms.ListView lvExamenesSeleccionados;
        private System.Windows.Forms.ColumnHeader chExamen;
        private System.Windows.Forms.ColumnHeader chCategoria;
        private System.Windows.Forms.TextBox txtExamen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader chMedicalExamId;
    }
}