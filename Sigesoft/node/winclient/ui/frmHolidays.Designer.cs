namespace Sigesoft.Node.WinClient.UI
{
    partial class frmHolidays
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_HolidayId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_Year");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Date");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Reason");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.grdHolidays = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cmHolidays = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.gbUpdateAdd = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMotivoAdd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtFechaAdd = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.grdHolidays)).BeginInit();
            this.cmHolidays.SuspendLayout();
            this.gbUpdateAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdHolidays
            // 
            this.grdHolidays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grdHolidays.CausesValidation = false;
            this.grdHolidays.ContextMenuStrip = this.cmHolidays;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdHolidays.DisplayLayout.Appearance = appearance1;
            ultraGridColumn7.Header.VisiblePosition = 0;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn11.Header.Caption = "Año";
            ultraGridColumn11.Header.VisiblePosition = 1;
            ultraGridColumn11.Width = 107;
            ultraGridColumn12.Header.Caption = "Fecha";
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn12.Width = 131;
            ultraGridColumn13.Header.Caption = "Motivo";
            ultraGridColumn13.Header.VisiblePosition = 3;
            ultraGridColumn13.Width = 264;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn7,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13});
            this.grdHolidays.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdHolidays.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grdHolidays.DisplayLayout.InterBandSpacing = 10;
            this.grdHolidays.DisplayLayout.MaxColScrollRegions = 1;
            this.grdHolidays.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdHolidays.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdHolidays.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdHolidays.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdHolidays.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdHolidays.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdHolidays.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdHolidays.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdHolidays.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdHolidays.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdHolidays.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdHolidays.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdHolidays.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdHolidays.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdHolidays.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdHolidays.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdHolidays.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdHolidays.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdHolidays.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdHolidays.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdHolidays.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdHolidays.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdHolidays.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdHolidays.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdHolidays.Location = new System.Drawing.Point(11, 42);
            this.grdHolidays.Margin = new System.Windows.Forms.Padding(2);
            this.grdHolidays.Name = "grdHolidays";
            this.grdHolidays.Size = new System.Drawing.Size(537, 249);
            this.grdHolidays.TabIndex = 101;
            this.grdHolidays.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdHolidays_AfterSelectChange);
            this.grdHolidays.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdHolidays_MouseDown);
            // 
            // cmHolidays
            // 
            this.cmHolidays.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemNuevo,
            this.itemEditar,
            this.itemEliminar});
            this.cmHolidays.Name = "cmHolidays";
            this.cmHolidays.Size = new System.Drawing.Size(118, 70);
            // 
            // itemNuevo
            // 
            this.itemNuevo.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.itemNuevo.Name = "itemNuevo";
            this.itemNuevo.Size = new System.Drawing.Size(117, 22);
            this.itemNuevo.Text = "Nuevo";
            this.itemNuevo.Click += new System.EventHandler(this.itemNuevo_Click);
            // 
            // itemEditar
            // 
            this.itemEditar.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.pencil;
            this.itemEditar.Name = "itemEditar";
            this.itemEditar.Size = new System.Drawing.Size(117, 22);
            this.itemEditar.Text = "Editar";
            this.itemEditar.Click += new System.EventHandler(this.itemEditar_Click);
            // 
            // itemEliminar
            // 
            this.itemEliminar.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.delete;
            this.itemEliminar.Name = "itemEliminar";
            this.itemEliminar.Size = new System.Drawing.Size(117, 22);
            this.itemEliminar.Text = "Eliminar";
            this.itemEliminar.Click += new System.EventHandler(this.itemEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.system_save;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(202, 142);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 103;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(57, 7);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(253, 20);
            this.txtReason.TabIndex = 104;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 106;
            this.label2.Text = "Motivo";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_search;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(316, 5);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 107;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // gbUpdateAdd
            // 
            this.gbUpdateAdd.Controls.Add(this.label3);
            this.gbUpdateAdd.Controls.Add(this.txtMotivoAdd);
            this.gbUpdateAdd.Controls.Add(this.label5);
            this.gbUpdateAdd.Controls.Add(this.dtFechaAdd);
            this.gbUpdateAdd.Controls.Add(this.btnGuardar);
            this.gbUpdateAdd.Enabled = false;
            this.gbUpdateAdd.Location = new System.Drawing.Point(565, 42);
            this.gbUpdateAdd.Name = "gbUpdateAdd";
            this.gbUpdateAdd.Size = new System.Drawing.Size(295, 174);
            this.gbUpdateAdd.TabIndex = 112;
            this.gbUpdateAdd.TabStop = false;
            this.gbUpdateAdd.Text = "Formulario";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 113;
            this.label3.Text = "Motivo";
            // 
            // txtMotivoAdd
            // 
            this.txtMotivoAdd.Location = new System.Drawing.Point(61, 71);
            this.txtMotivoAdd.Multiline = true;
            this.txtMotivoAdd.Name = "txtMotivoAdd";
            this.txtMotivoAdd.Size = new System.Drawing.Size(216, 50);
            this.txtMotivoAdd.TabIndex = 112;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 111;
            this.label5.Text = "Fecha";
            // 
            // dtFechaAdd
            // 
            this.dtFechaAdd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaAdd.Location = new System.Drawing.Point(61, 31);
            this.dtFechaAdd.Name = "dtFechaAdd";
            this.dtFechaAdd.Size = new System.Drawing.Size(99, 20);
            this.dtFechaAdd.TabIndex = 109;
            // 
            // frmHolidays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 302);
            this.Controls.Add(this.gbUpdateAdd);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.grdHolidays);
            this.Name = "frmHolidays";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Feriados";
            this.Load += new System.EventHandler(this.frmHolidays_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdHolidays)).EndInit();
            this.cmHolidays.ResumeLayout(false);
            this.gbUpdateAdd.ResumeLayout(false);
            this.gbUpdateAdd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdHolidays;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmHolidays;
        private System.Windows.Forms.ToolStripMenuItem itemNuevo;
        private System.Windows.Forms.ToolStripMenuItem itemEditar;
        private System.Windows.Forms.ToolStripMenuItem itemEliminar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox gbUpdateAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMotivoAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtFechaAdd;

    }
}