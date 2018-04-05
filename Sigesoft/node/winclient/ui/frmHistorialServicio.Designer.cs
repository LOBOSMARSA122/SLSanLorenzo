namespace Sigesoft.Node.WinClient.UI
{
    partial class frmHistorialServicio
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_MasterServiceName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceStatusName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_DateTimeCalendar");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_OrganizationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_LocationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceTypeName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.grdDataService = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblRecordCountService = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataService)).BeginInit();
            this.SuspendLayout();
            // 
            // grdDataService
            // 
            this.grdDataService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataService.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataService.DisplayLayout.Appearance = appearance1;
            ultraGridColumn13.Header.Caption = "Servicio";
            ultraGridColumn13.Header.VisiblePosition = 2;
            ultraGridColumn13.Width = 156;
            ultraGridColumn14.Header.Caption = "Estado Servico";
            ultraGridColumn14.Header.VisiblePosition = 3;
            ultraGridColumn14.Width = 174;
            ultraGridColumn15.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn15.Header.Caption = "Fecha Cita";
            ultraGridColumn15.Header.VisiblePosition = 0;
            ultraGridColumn15.Width = 153;
            ultraGridColumn16.Header.Caption = "Empresa";
            ultraGridColumn16.Header.VisiblePosition = 4;
            ultraGridColumn18.Header.Caption = "Sede";
            ultraGridColumn18.Header.VisiblePosition = 5;
            ultraGridColumn12.Header.Caption = "Tipo Servicio";
            ultraGridColumn12.Header.VisiblePosition = 1;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn18,
            ultraGridColumn12});
            this.grdDataService.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataService.DisplayLayout.InterBandSpacing = 10;
            this.grdDataService.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataService.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataService.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataService.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataService.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataService.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataService.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataService.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataService.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDataService.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataService.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDataService.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataService.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDataService.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataService.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataService.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDataService.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdDataService.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDataService.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataService.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataService.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataService.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataService.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataService.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataService.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataService.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataService.Location = new System.Drawing.Point(11, 11);
            this.grdDataService.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataService.Name = "grdDataService";
            this.grdDataService.Size = new System.Drawing.Size(639, 346);
            this.grdDataService.TabIndex = 46;
            // 
            // lblRecordCountService
            // 
            this.lblRecordCountService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountService.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCountService.Location = new System.Drawing.Point(379, 361);
            this.lblRecordCountService.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountService.Name = "lblRecordCountService";
            this.lblRecordCountService.Size = new System.Drawing.Size(271, 30);
            this.lblRecordCountService.TabIndex = 47;
            this.lblRecordCountService.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountService.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmHistorialServicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 398);
            this.Controls.Add(this.lblRecordCountService);
            this.Controls.Add(this.grdDataService);
            this.Name = "frmHistorialServicio";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial de Servicios";
            ((System.ComponentModel.ISupportInitialize)(this.grdDataService)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataService;
        private System.Windows.Forms.Label lblRecordCountService;
    }
}