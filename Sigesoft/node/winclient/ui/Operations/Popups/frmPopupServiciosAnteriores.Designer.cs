namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    partial class frmPopupServiciosAnteriores
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn94 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn95 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_MasterServiceName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn96 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_ServiceDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn97 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiseaseName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.gbServiciosAnteriores = new System.Windows.Forms.GroupBox();
            this.btnDescargarServicio = new System.Windows.Forms.Button();
            this.btnVerServicioAnterior = new System.Windows.Forms.Button();
            this.grdServiciosAnteriores = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblGes = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.gbServiciosAnteriores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiciosAnteriores)).BeginInit();
            this.SuspendLayout();
            // 
            // gbServiciosAnteriores
            // 
            this.gbServiciosAnteriores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbServiciosAnteriores.Controls.Add(this.btnDescargarServicio);
            this.gbServiciosAnteriores.Controls.Add(this.btnVerServicioAnterior);
            this.gbServiciosAnteriores.Controls.Add(this.grdServiciosAnteriores);
            this.gbServiciosAnteriores.Controls.Add(this.lblGes);
            this.gbServiciosAnteriores.Controls.Add(this.label26);
            this.gbServiciosAnteriores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbServiciosAnteriores.ForeColor = System.Drawing.Color.MediumBlue;
            this.gbServiciosAnteriores.Location = new System.Drawing.Point(12, 12);
            this.gbServiciosAnteriores.Name = "gbServiciosAnteriores";
            this.gbServiciosAnteriores.Size = new System.Drawing.Size(736, 480);
            this.gbServiciosAnteriores.TabIndex = 5;
            this.gbServiciosAnteriores.TabStop = false;
            this.gbServiciosAnteriores.Text = "Servicios Anteriores";
            // 
            // btnDescargarServicio
            // 
            this.btnDescargarServicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDescargarServicio.BackColor = System.Drawing.SystemColors.Control;
            this.btnDescargarServicio.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDescargarServicio.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnDescargarServicio.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnDescargarServicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargarServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargarServicio.ForeColor = System.Drawing.Color.Black;
            this.btnDescargarServicio.Image = global::Sigesoft.Node.WinClient.UI.Resources.server_add;
            this.btnDescargarServicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDescargarServicio.Location = new System.Drawing.Point(275, 15);
            this.btnDescargarServicio.Margin = new System.Windows.Forms.Padding(2);
            this.btnDescargarServicio.Name = "btnDescargarServicio";
            this.btnDescargarServicio.Size = new System.Drawing.Size(128, 24);
            this.btnDescargarServicio.TabIndex = 94;
            this.btnDescargarServicio.Text = "Descargar Servicio";
            this.btnDescargarServicio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDescargarServicio.UseVisualStyleBackColor = false;
            this.btnDescargarServicio.Click += new System.EventHandler(this.btnDescargarServicio_Click);
            // 
            // btnVerServicioAnterior
            // 
            this.btnVerServicioAnterior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVerServicioAnterior.BackColor = System.Drawing.SystemColors.Control;
            this.btnVerServicioAnterior.Enabled = false;
            this.btnVerServicioAnterior.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnVerServicioAnterior.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnVerServicioAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnVerServicioAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerServicioAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerServicioAnterior.ForeColor = System.Drawing.Color.Black;
            this.btnVerServicioAnterior.Image = global::Sigesoft.Node.WinClient.UI.Resources.application_osx_start;
            this.btnVerServicioAnterior.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerServicioAnterior.Location = new System.Drawing.Point(597, 19);
            this.btnVerServicioAnterior.Margin = new System.Windows.Forms.Padding(2);
            this.btnVerServicioAnterior.Name = "btnVerServicioAnterior";
            this.btnVerServicioAnterior.Size = new System.Drawing.Size(134, 24);
            this.btnVerServicioAnterior.TabIndex = 93;
            this.btnVerServicioAnterior.Text = "Ver servicio Anterior";
            this.btnVerServicioAnterior.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVerServicioAnterior.UseVisualStyleBackColor = false;
            this.btnVerServicioAnterior.Click += new System.EventHandler(this.btnVerServicioAnterior_Click);
            // 
            // grdServiciosAnteriores
            // 
            this.grdServiciosAnteriores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdServiciosAnteriores.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdServiciosAnteriores.DisplayLayout.Appearance = appearance1;
            ultraGridColumn94.Header.VisiblePosition = 0;
            ultraGridColumn94.Hidden = true;
            ultraGridColumn95.Header.Caption = "Servicio";
            ultraGridColumn95.Header.VisiblePosition = 2;
            ultraGridColumn95.Width = 169;
            ultraGridColumn96.Header.Caption = "Fecha";
            ultraGridColumn96.Header.VisiblePosition = 1;
            ultraGridColumn96.Width = 71;
            ultraGridColumn97.Header.Caption = "Diagnóstico";
            ultraGridColumn97.Header.VisiblePosition = 3;
            ultraGridColumn97.Width = 348;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn94,
            ultraGridColumn95,
            ultraGridColumn96,
            ultraGridColumn97});
            this.grdServiciosAnteriores.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdServiciosAnteriores.DisplayLayout.InterBandSpacing = 10;
            this.grdServiciosAnteriores.DisplayLayout.MaxColScrollRegions = 1;
            this.grdServiciosAnteriores.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdServiciosAnteriores.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdServiciosAnteriores.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdServiciosAnteriores.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdServiciosAnteriores.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdServiciosAnteriores.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdServiciosAnteriores.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdServiciosAnteriores.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdServiciosAnteriores.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdServiciosAnteriores.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdServiciosAnteriores.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdServiciosAnteriores.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdServiciosAnteriores.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdServiciosAnteriores.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdServiciosAnteriores.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdServiciosAnteriores.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdServiciosAnteriores.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdServiciosAnteriores.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdServiciosAnteriores.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdServiciosAnteriores.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdServiciosAnteriores.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdServiciosAnteriores.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdServiciosAnteriores.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdServiciosAnteriores.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdServiciosAnteriores.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdServiciosAnteriores.Location = new System.Drawing.Point(5, 47);
            this.grdServiciosAnteriores.Margin = new System.Windows.Forms.Padding(2);
            this.grdServiciosAnteriores.Name = "grdServiciosAnteriores";
            this.grdServiciosAnteriores.Size = new System.Drawing.Size(728, 428);
            this.grdServiciosAnteriores.TabIndex = 58;
            this.grdServiciosAnteriores.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdServiciosAnteriores_AfterSelectChange);
            this.grdServiciosAnteriores.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdServiciosAnteriores_MouseDown);
            // 
            // lblGes
            // 
            this.lblGes.BackColor = System.Drawing.Color.White;
            this.lblGes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGes.ForeColor = System.Drawing.Color.Black;
            this.lblGes.Location = new System.Drawing.Point(496, 23);
            this.lblGes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGes.Name = "lblGes";
            this.lblGes.Size = new System.Drawing.Size(83, 20);
            this.lblGes.TabIndex = 28;
            this.lblGes.Text = "lblGes";
            this.lblGes.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(460, 26);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(32, 13);
            this.label26.TabIndex = 27;
            this.label26.Text = "GES";
            this.label26.Visible = false;
            // 
            // frmPopupServiciosAnteriores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 504);
            this.Controls.Add(this.gbServiciosAnteriores);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPopupServiciosAnteriores";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servicios Anteriores";
            this.Load += new System.EventHandler(this.frmPopupServiciosAnteriores_Load);
            this.gbServiciosAnteriores.ResumeLayout(false);
            this.gbServiciosAnteriores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiciosAnteriores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbServiciosAnteriores;
        private System.Windows.Forms.Button btnVerServicioAnterior;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdServiciosAnteriores;
        private System.Windows.Forms.Label lblGes;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnDescargarServicio;
    }
}