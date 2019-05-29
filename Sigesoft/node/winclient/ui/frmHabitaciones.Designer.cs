namespace Sigesoft.Node.WinClient.UI
{
    partial class frmHabitaciones
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Habitacion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Estado");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_HabitacionId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_HospHabitacionId");
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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHabitaciones));
            this.grdDataHabitaciones = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbPaciente = new System.Windows.Forms.RadioButton();
            this.rbMedicoTratante = new System.Windows.Forms.RadioButton();
            this.txtUnidProdId = new System.Windows.Forms.TextBox();
            this.cbLine = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardarTicket = new System.Windows.Forms.Button();
            this.gbForm = new System.Windows.Forms.GroupBox();
            this.cmEstadosHabitacion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemLiberar = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataHabitaciones)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLine)).BeginInit();
            this.gbForm.SuspendLayout();
            this.cmEstadosHabitacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdDataHabitaciones
            // 
            this.grdDataHabitaciones.CausesValidation = false;
            this.grdDataHabitaciones.ContextMenuStrip = this.cmEstadosHabitacion;
            this.grdDataHabitaciones.DataMember = null;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataHabitaciones.DisplayLayout.Appearance = appearance1;
            ultraGridColumn28.Header.VisiblePosition = 0;
            ultraGridColumn28.Width = 140;
            ultraGridColumn29.Header.VisiblePosition = 1;
            ultraGridColumn29.Width = 238;
            ultraGridColumn1.Header.VisiblePosition = 2;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 3;
            ultraGridColumn2.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn1,
            ultraGridColumn2});
            this.grdDataHabitaciones.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataHabitaciones.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataHabitaciones.DisplayLayout.InterBandSpacing = 10;
            this.grdDataHabitaciones.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataHabitaciones.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataHabitaciones.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataHabitaciones.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataHabitaciones.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataHabitaciones.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataHabitaciones.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataHabitaciones.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataHabitaciones.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDataHabitaciones.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataHabitaciones.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDataHabitaciones.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataHabitaciones.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDataHabitaciones.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataHabitaciones.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataHabitaciones.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDataHabitaciones.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdDataHabitaciones.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDataHabitaciones.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataHabitaciones.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataHabitaciones.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataHabitaciones.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataHabitaciones.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataHabitaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataHabitaciones.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdDataHabitaciones.Location = new System.Drawing.Point(11, 11);
            this.grdDataHabitaciones.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataHabitaciones.Name = "grdDataHabitaciones";
            this.grdDataHabitaciones.Size = new System.Drawing.Size(425, 263);
            this.grdDataHabitaciones.TabIndex = 45;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(112, 31);
            this.dtpFechaInicio.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(122, 21);
            this.dtpFechaInicio.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 109;
            this.label2.Text = "Fecha Inicio";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(112, 60);
            this.dtpFechaFin.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.ShowCheckBox = true;
            this.dtpFechaFin.Size = new System.Drawing.Size(122, 20);
            this.dtpFechaFin.TabIndex = 112;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 111;
            this.label3.Text = "Fecha Fin";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(112, 91);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(122, 20);
            this.txtPrecio.TabIndex = 114;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 113;
            this.label4.Text = "Precio por día";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbPaciente);
            this.groupBox3.Controls.Add(this.rbMedicoTratante);
            this.groupBox3.Location = new System.Drawing.Point(6, 118);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(248, 49);
            this.groupBox3.TabIndex = 115;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Con Cargo a:";
            // 
            // rbPaciente
            // 
            this.rbPaciente.AutoSize = true;
            this.rbPaciente.Location = new System.Drawing.Point(159, 22);
            this.rbPaciente.Name = "rbPaciente";
            this.rbPaciente.Size = new System.Drawing.Size(67, 17);
            this.rbPaciente.TabIndex = 10;
            this.rbPaciente.TabStop = true;
            this.rbPaciente.Text = "Paciente";
            this.rbPaciente.UseVisualStyleBackColor = true;
            // 
            // rbMedicoTratante
            // 
            this.rbMedicoTratante.AutoSize = true;
            this.rbMedicoTratante.Location = new System.Drawing.Point(6, 22);
            this.rbMedicoTratante.Name = "rbMedicoTratante";
            this.rbMedicoTratante.Size = new System.Drawing.Size(103, 17);
            this.rbMedicoTratante.TabIndex = 9;
            this.rbMedicoTratante.TabStop = true;
            this.rbMedicoTratante.Text = "Medico Tratante";
            this.rbMedicoTratante.UseVisualStyleBackColor = true;
            // 
            // txtUnidProdId
            // 
            this.txtUnidProdId.Location = new System.Drawing.Point(87, 236);
            this.txtUnidProdId.Name = "txtUnidProdId";
            this.txtUnidProdId.Size = new System.Drawing.Size(80, 20);
            this.txtUnidProdId.TabIndex = 132;
            this.txtUnidProdId.Visible = false;
            // 
            // cbLine
            // 
            appearance8.BackColor = System.Drawing.SystemColors.Window;
            appearance8.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbLine.DisplayLayout.Appearance = appearance8;
            this.cbLine.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cbLine.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance9.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.cbLine.DisplayLayout.GroupByBox.Appearance = appearance9;
            appearance10.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cbLine.DisplayLayout.GroupByBox.BandLabelAppearance = appearance10;
            this.cbLine.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance11.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance11.BackColor2 = System.Drawing.SystemColors.Control;
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cbLine.DisplayLayout.GroupByBox.PromptAppearance = appearance11;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbLine.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance13.BackColor = System.Drawing.SystemColors.Highlight;
            appearance13.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cbLine.DisplayLayout.Override.ActiveRowAppearance = appearance13;
            this.cbLine.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            this.cbLine.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cbLine.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance14.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine.DisplayLayout.Override.CardAreaAppearance = appearance14;
            appearance15.BorderColor = System.Drawing.Color.Silver;
            appearance15.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cbLine.DisplayLayout.Override.CellAppearance = appearance15;
            this.cbLine.DisplayLayout.Override.CellPadding = 0;
            this.cbLine.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.EntireColumn;
            appearance16.BackColor = System.Drawing.SystemColors.Control;
            appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance16.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.BorderColor = System.Drawing.SystemColors.Window;
            this.cbLine.DisplayLayout.Override.GroupByRowAppearance = appearance16;
            appearance17.TextHAlignAsString = "Left";
            this.cbLine.DisplayLayout.Override.HeaderAppearance = appearance17;
            this.cbLine.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance18.BackColor = System.Drawing.SystemColors.Window;
            appearance18.BorderColor = System.Drawing.Color.Silver;
            this.cbLine.DisplayLayout.Override.RowAppearance = appearance18;
            appearance19.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbLine.DisplayLayout.Override.TemplateAddRowAppearance = appearance19;
            this.cbLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLine.Location = new System.Drawing.Point(6, 186);
            this.cbLine.Name = "cbLine";
            this.cbLine.Size = new System.Drawing.Size(248, 22);
            this.cbLine.TabIndex = 131;
            this.cbLine.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.cbLine_RowSelected);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Black;
            this.btnSalir.Image = global::Sigesoft.Node.WinClient.UI.Resources.bullet_cross;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(6, 234);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(76, 23);
            this.btnSalir.TabIndex = 130;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGuardarTicket
            // 
            this.btnGuardarTicket.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarTicket.Image")));
            this.btnGuardarTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarTicket.Location = new System.Drawing.Point(178, 234);
            this.btnGuardarTicket.Name = "btnGuardarTicket";
            this.btnGuardarTicket.Size = new System.Drawing.Size(76, 23);
            this.btnGuardarTicket.TabIndex = 129;
            this.btnGuardarTicket.Text = "Guardar";
            this.btnGuardarTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarTicket.UseVisualStyleBackColor = true;
            this.btnGuardarTicket.Click += new System.EventHandler(this.btnGuardarTicket_Click);
            // 
            // gbForm
            // 
            this.gbForm.Controls.Add(this.label2);
            this.gbForm.Controls.Add(this.txtUnidProdId);
            this.gbForm.Controls.Add(this.dtpFechaInicio);
            this.gbForm.Controls.Add(this.btnSalir);
            this.gbForm.Controls.Add(this.btnGuardarTicket);
            this.gbForm.Controls.Add(this.cbLine);
            this.gbForm.Controls.Add(this.label3);
            this.gbForm.Controls.Add(this.dtpFechaFin);
            this.gbForm.Controls.Add(this.label4);
            this.gbForm.Controls.Add(this.groupBox3);
            this.gbForm.Controls.Add(this.txtPrecio);
            this.gbForm.Location = new System.Drawing.Point(441, 11);
            this.gbForm.Name = "gbForm";
            this.gbForm.Size = new System.Drawing.Size(260, 262);
            this.gbForm.TabIndex = 133;
            this.gbForm.TabStop = false;
            this.gbForm.Text = "Datos";
            // 
            // cmEstadosHabitacion
            // 
            this.cmEstadosHabitacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemLiberar});
            this.cmEstadosHabitacion.Name = "cmEstadosHabitacion";
            this.cmEstadosHabitacion.Size = new System.Drawing.Size(172, 26);
            // 
            // itemLiberar
            // 
            this.itemLiberar.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.itemLiberar.Name = "itemLiberar";
            this.itemLiberar.Size = new System.Drawing.Size(171, 22);
            this.itemLiberar.Text = "Liberar Habitacion";
            this.itemLiberar.Click += new System.EventHandler(this.itemLiberar_Click);
            // 
            // frmHabitaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 285);
            this.Controls.Add(this.gbForm);
            this.Controls.Add(this.grdDataHabitaciones);
            this.Name = "frmHabitaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmHabitaciones";
            this.Load += new System.EventHandler(this.frmHabitaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataHabitaciones)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLine)).EndInit();
            this.gbForm.ResumeLayout(false);
            this.gbForm.PerformLayout();
            this.cmEstadosHabitacion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataHabitaciones;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbPaciente;
        private System.Windows.Forms.RadioButton rbMedicoTratante;
        private System.Windows.Forms.TextBox txtUnidProdId;
        private Infragistics.Win.UltraWinGrid.UltraCombo cbLine;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardarTicket;
        private System.Windows.Forms.GroupBox gbForm;
        private System.Windows.Forms.ContextMenuStrip cmEstadosHabitacion;
        private System.Windows.Forms.ToolStripMenuItem itemLiberar;
    }
}