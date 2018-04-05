namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    partial class frmPopupAntecedentes
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn90 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_AntecedentTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn91 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DateOrGroup");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn92 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_StartDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn93 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiseasesName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.gbAntecedentes = new System.Windows.Forms.GroupBox();
            this.btnVerEditarAntecedentes = new System.Windows.Forms.Button();
            this.grdAntecedentes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.gbAntecedentes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAntecedentes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbAntecedentes
            // 
            this.gbAntecedentes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAntecedentes.Controls.Add(this.btnVerEditarAntecedentes);
            this.gbAntecedentes.Controls.Add(this.grdAntecedentes);
            this.gbAntecedentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAntecedentes.ForeColor = System.Drawing.Color.MediumBlue;
            this.gbAntecedentes.Location = new System.Drawing.Point(12, 12);
            this.gbAntecedentes.Name = "gbAntecedentes";
            this.gbAntecedentes.Size = new System.Drawing.Size(725, 230);
            this.gbAntecedentes.TabIndex = 4;
            this.gbAntecedentes.TabStop = false;
            this.gbAntecedentes.Text = "Antecedentes";
            // 
            // btnVerEditarAntecedentes
            // 
            this.btnVerEditarAntecedentes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerEditarAntecedentes.BackColor = System.Drawing.SystemColors.Control;
            this.btnVerEditarAntecedentes.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnVerEditarAntecedentes.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnVerEditarAntecedentes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnVerEditarAntecedentes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerEditarAntecedentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerEditarAntecedentes.ForeColor = System.Drawing.Color.Black;
            this.btnVerEditarAntecedentes.Image = global::Sigesoft.Node.WinClient.UI.Resources.book_open;
            this.btnVerEditarAntecedentes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerEditarAntecedentes.Location = new System.Drawing.Point(532, 13);
            this.btnVerEditarAntecedentes.Margin = new System.Windows.Forms.Padding(2);
            this.btnVerEditarAntecedentes.Name = "btnVerEditarAntecedentes";
            this.btnVerEditarAntecedentes.Size = new System.Drawing.Size(181, 24);
            this.btnVerEditarAntecedentes.TabIndex = 92;
            this.btnVerEditarAntecedentes.Text = "&Agregar / Editar Antecedentes";
            this.btnVerEditarAntecedentes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVerEditarAntecedentes.UseVisualStyleBackColor = false;
            this.btnVerEditarAntecedentes.Click += new System.EventHandler(this.btnVerEditarAntecedentes_Click);
            // 
            // grdAntecedentes
            // 
            this.grdAntecedentes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAntecedentes.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdAntecedentes.DisplayLayout.Appearance = appearance1;
            ultraGridColumn90.Header.Caption = "Tipo de Ant.";
            ultraGridColumn90.Header.VisiblePosition = 1;
            ultraGridColumn90.Width = 196;
            ultraGridColumn91.Header.Caption = "Fecha / Grupo";
            ultraGridColumn91.Header.VisiblePosition = 0;
            ultraGridColumn91.Width = 113;
            ultraGridColumn92.Header.VisiblePosition = 2;
            ultraGridColumn92.Hidden = true;
            ultraGridColumn93.Header.Caption = "Descripción";
            ultraGridColumn93.Header.VisiblePosition = 3;
            ultraGridColumn93.Width = 646;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn90,
            ultraGridColumn91,
            ultraGridColumn92,
            ultraGridColumn93});
            this.grdAntecedentes.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdAntecedentes.DisplayLayout.InterBandSpacing = 10;
            this.grdAntecedentes.DisplayLayout.MaxColScrollRegions = 1;
            this.grdAntecedentes.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdAntecedentes.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdAntecedentes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdAntecedentes.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdAntecedentes.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdAntecedentes.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdAntecedentes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdAntecedentes.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdAntecedentes.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdAntecedentes.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdAntecedentes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdAntecedentes.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdAntecedentes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdAntecedentes.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdAntecedentes.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdAntecedentes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdAntecedentes.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdAntecedentes.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdAntecedentes.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdAntecedentes.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdAntecedentes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAntecedentes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAntecedentes.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdAntecedentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdAntecedentes.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdAntecedentes.Location = new System.Drawing.Point(10, 47);
            this.grdAntecedentes.Margin = new System.Windows.Forms.Padding(2);
            this.grdAntecedentes.Name = "grdAntecedentes";
            this.grdAntecedentes.Size = new System.Drawing.Size(703, 178);
            this.grdAntecedentes.TabIndex = 57;
            // 
            // frmPopupAntecedentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 254);
            this.Controls.Add(this.gbAntecedentes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPopupAntecedentes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Antecedentes";
            this.Load += new System.EventHandler(this.frmPopupAntecedentes_Load);
            this.gbAntecedentes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAntecedentes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAntecedentes;
        private System.Windows.Forms.Button btnVerEditarAntecedentes;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdAntecedentes;
    }
}