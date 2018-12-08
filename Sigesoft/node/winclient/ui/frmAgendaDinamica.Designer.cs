namespace Sigesoft.Node.WinClient.UI
{
    partial class FrmAgendaDinamica
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NroDocumento");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Nombre");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ApellidoPaterno");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ApellidoMaterno");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FechaNacimiento");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GeneroId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Puesto");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Seleccionar", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Clonar", 1);
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            this.grdSchedule = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnAgregarRegistro = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // grdSchedule
            // 
            this.grdSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSchedule.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdSchedule.DisplayLayout.Appearance = appearance1;
            appearance2.FontData.SizeInPoints = 9F;
            ultraGridColumn12.CellAppearance = appearance2;
            ultraGridColumn12.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            ultraGridColumn12.Header.VisiblePosition = 1;
            ultraGridColumn12.MaxLength = 11;
            ultraGridColumn13.Header.VisiblePosition = 2;
            ultraGridColumn14.Header.VisiblePosition = 3;
            ultraGridColumn15.Header.VisiblePosition = 4;
            ultraGridColumn18.Header.VisiblePosition = 5;
            ultraGridColumn18.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
            ultraGridColumn3.Header.VisiblePosition = 6;
            ultraGridColumn27.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            ultraGridColumn27.Header.VisiblePosition = 7;
            ultraGridColumn1.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn1.Width = 80;
            ultraGridColumn2.Header.VisiblePosition = 8;
            ultraGridColumn2.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn18,
            ultraGridColumn3,
            ultraGridColumn27,
            ultraGridColumn1,
            ultraGridColumn2});
            ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.Yes;
            this.grdSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdSchedule.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedule.DisplayLayout.GroupByBox.Prompt = " ";
            this.grdSchedule.DisplayLayout.InterBandSpacing = 10;
            this.grdSchedule.DisplayLayout.MaxColScrollRegions = 1;
            this.grdSchedule.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdSchedule.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdSchedule.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdSchedule.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.grdSchedule.DisplayLayout.Override.CardAreaAppearance = appearance3;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.White;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdSchedule.DisplayLayout.Override.CellAppearance = appearance4;
            this.grdSchedule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdSchedule.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.grdSchedule.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance5.BackColor = System.Drawing.Color.White;
            appearance5.BackColor2 = System.Drawing.Color.LightGray;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance5.BorderColor = System.Drawing.Color.DarkGray;
            appearance5.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdSchedule.DisplayLayout.Override.HeaderAppearance = appearance5;
            this.grdSchedule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance6.AlphaLevel = ((short)(187));
            appearance6.BackColor = System.Drawing.Color.Gainsboro;
            appearance6.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance6.ForeColor = System.Drawing.Color.Black;
            appearance6.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdSchedule.DisplayLayout.Override.RowAlternateAppearance = appearance6;
            appearance7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdSchedule.DisplayLayout.Override.RowSelectorAppearance = appearance7;
            this.grdSchedule.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.RowIndex;
            this.grdSchedule.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            appearance8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance8.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance8.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance8.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance8.FontData.BoldAsString = "False";
            appearance8.ForeColor = System.Drawing.Color.Black;
            this.grdSchedule.DisplayLayout.Override.SelectedRowAppearance = appearance8;
            this.grdSchedule.DisplayLayout.Override.SupportDataErrorInfo = Infragistics.Win.UltraWinGrid.SupportDataErrorInfo.RowsAndCells;
            this.grdSchedule.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdSchedule.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdSchedule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSchedule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSchedule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdSchedule.Location = new System.Drawing.Point(11, 11);
            this.grdSchedule.Margin = new System.Windows.Forms.Padding(2);
            this.grdSchedule.Name = "grdSchedule";
            this.grdSchedule.Size = new System.Drawing.Size(876, 414);
            this.grdSchedule.TabIndex = 154;
            this.grdSchedule.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdSchedule_InitializeLayout);
            this.grdSchedule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdSchedule_KeyDown);
            this.grdSchedule.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdSchedule_KeyUp);
            // 
            // btnAgregarRegistro
            // 
            this.btnAgregarRegistro.Location = new System.Drawing.Point(812, 430);
            this.btnAgregarRegistro.Name = "btnAgregarRegistro";
            this.btnAgregarRegistro.Size = new System.Drawing.Size(75, 23);
            this.btnAgregarRegistro.TabIndex = 155;
            this.btnAgregarRegistro.Text = "Agregar";
            this.btnAgregarRegistro.UseVisualStyleBackColor = true;
            this.btnAgregarRegistro.Click += new System.EventHandler(this.btnAgregarRegistro_Click);
            // 
            // FrmAgendaDinamica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 478);
            this.Controls.Add(this.btnAgregarRegistro);
            this.Controls.Add(this.grdSchedule);
            this.Name = "FrmAgendaDinamica";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agenda Dinamica";
            this.Load += new System.EventHandler(this.frmAgendaDinamica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdSchedule;
        private System.Windows.Forms.Button btnAgregarRegistro;
    }
}